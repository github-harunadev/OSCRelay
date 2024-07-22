using Newtonsoft.Json.Linq;
using OscCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using Valve.VR;
using Windows.Media.Protection.PlayReady;
using Windows.Storage.Streams;

namespace harunadev.OSCRelay
{
    public partial class MainWindow : Window
    {
        readonly string ApplicationKey = "harunadev.OSCRelay";

        UdpClient Listener;
        UdpClient Sender;
        List<IPEndPoint> SenderPoints = new List<IPEndPoint>();

        int ListenerPort = 9001;
        Task ListenerTask;
        Timer ListenerPPSTimer;

        bool UseOpenVR = false;
        CVRSystem? OVRSystem;


        public MainWindow()
        {
            InitializeComponent();
            LoadConfig();
            RegisterListener();
            RefreshSender();
            ListenerTask = Task.Run(() => ListenerProcess());
            ListenerPPSTimer = new Timer();
            ListenerPPSTimer.Interval = 1000;
            ListenerPPSTimer.Elapsed += ListenerPPSTimer_Elapsed;
            ListenerPPSTimer.Start();

            CompositionTarget.Rendering += UpdateStatus;
        }


        void UpdateStatus(object sender, EventArgs e)
        {
            if (Listener != null)
            {
                ConnectStatusText.Text = $"Listening from {IPAddress.Loopback}:{ListenerPort}";
                ConnectToggleButton.Content = "Listening";
                ConnectToggleButton.IsChecked = true;
            } else
            {
                ConnectStatusText.Text = "Not listening!";
                ConnectToggleButton.Content = "Not Listening";
                ConnectToggleButton.IsChecked = false;
            }
            StatusText.Text = $"{_processingPerSecond} messages received last second, {_sentPerSecond} message sent last second, total {_totalErrors} error(s) in working thread";
            SenderTargetListParseErrorText.Text = ParseErrorText;
        }

        void ToggleListener()
        {
            if (Listener != null)
            {
                DecommisionListener();
            }
            else
            {
                RegisterListener();
            }
        }
        bool TriggerRegister = true;
        void RegisterListener()
        {
            TriggerRegister = true;
        }
        bool TriggerDecomission = false;
        void DecommisionListener()
        {
            TriggerDecomission = true;
        }

        string ParseErrorText = "";
        void RefreshSender()
        {
            SenderPoints.Clear();

            ParseErrorText = "";

            string unparsed = SenderTargetList.Text.Trim();
            string[] split = unparsed.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < split.Length; i++)
                split[i] = split[i].Trim();

            int pass = 0;
            int error = 0;
            for (int i = 0; i < split.Length; ++i)
            {
                if (split[i].Length < 2) continue;
                try
                {
                    string _address = split[i].Substring(0, split[i].IndexOf(":"));
                    IPAddress address = IPAddress.Parse(_address);
                    string _port = split[i].Substring(split[i].IndexOf(":") + 1);
                    int port = Convert.ToInt32(_port);

                    if (port == ListenerPort && IPAddress.IsLoopback(address))
                    {
                        throw new Exception("IP and port cannot be same as Listeners.");
                    }

                    IPEndPoint remoteEndPoint = new IPEndPoint(address, port);
                    SenderPoints.Add(remoteEndPoint);

                    pass++;
                } catch (Exception e)
                {
                    ParseErrorText += $"{split[i]}\n\t:{e.Message}\n";
                    error++;
                }
            }
            SenderTargetListParseResultText.Text = $"{pass} pair(s) successfully parsed, {error} pair(s) failed";
        }

        int _messageProcessed = 0;
        int _sentSuccess = 0;
        int _processingPerSecond = 0;
        int _sentPerSecond = 0;
        int _totalErrors = 0;
        private async void ListenerProcess()
        {
            while (true)
            {
                if (Listener != null && Sender != null)
                {
                    try
                    {
                        UdpReceiveResult result = await Listener.ReceiveAsync();
                        byte[] packet = OscPacket.Read(result.Buffer, 0, result.Buffer.Length).ToByteArray();

                        for (int i = 0; i < SenderPoints.Count; i++)
                        {
                            if (i >= SenderPoints.Count) break;
                            if (Sender != null)
                            {
                                Sender.Send(packet, packet.Length, SenderPoints[i]);
                                _sentSuccess++;
                            }
                        }
                        _messageProcessed++;
                    }
                    catch (Exception ex)
                    {
                        _totalErrors++;
                    }
                }

                if (TriggerDecomission)
                {
                    if (Listener != null)
                    {
                        Listener.Dispose();
                        Listener = null;
                    }
                    if (Sender != null)
                    {
                        Sender.Dispose();
                        Sender = null;
                    }
                    TriggerDecomission = false;
                }

                if (TriggerRegister)
                {
                    if (Listener == null)
                    {
                        Listener = new UdpClient(ListenerPort);
                    }
                    if (Sender == null)
                    {
                        Sender = new UdpClient();
                    }
                    TriggerRegister = false;
                }
            }
        }
        void ListenerPPSTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _processingPerSecond = _messageProcessed;
            _sentPerSecond = _sentSuccess;
            _messageProcessed = 0;
            _sentSuccess = 0;
        }

        void LoadConfig()
        {
            try
            {
                string dataRaw = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"));

                JObject data = JObject.Parse(dataRaw);

                if (data.ContainsKey("listenPort"))
                    ListenerPort = ((int)data["listenPort"]);
                if (data.ContainsKey("sendPairs"))
                {
                    List<JToken> tmp = data["sendPairs"].ToList();
                    StringBuilder builder = new StringBuilder();
                    foreach (JToken token in tmp)
                    {
                        builder.Append(((string)token) + Environment.NewLine);
                    }
                    SenderTargetList.Text = builder.ToString();
                }
                if (data.ContainsKey("useOpenVR"))
                    UseOpenVR = ((bool)data["useOpenVR"]);

                InitializeOpenVR();

                RefreshSender();
                DecommisionListener();
                RegisterListener();
            }
            catch (Exception ex)
            {
                SaveConfig();
            }
        }

        void SaveConfig()
        {
            JArray sendPair = new JArray();
            for (int i = 0; i < SenderPoints.Count; i++)
            {
                sendPair.Add(SenderPoints[i].Address.ToString() + ":" + SenderPoints[i].Port);
            }

            JObject output = new JObject()
            {
                new JProperty("listenPort", ListenerPort),
                new JProperty("sendPairs", sendPair),
                new JProperty("useOpenVR", UseOpenVR)
            };

            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"), output.ToString());
        }

        void InitializeOpenVR()
        {
            if (UseOpenVR)
            {
                SetOpenVRAutoLaunch();
            }
        }

        void SetOpenVRAutoLaunch()
        {
            var initerr = EVRInitError.None;
            OVRSystem = OpenVR.Init(ref initerr, EVRApplicationType.VRApplication_Overlay);
            
            if (initerr != EVRInitError.None)
            {
                return;
            }
            
            if (OpenVR.Applications.IsApplicationInstalled(ApplicationKey))
            {
                StringBuilder oldWd = new StringBuilder();
                var wderror = EVRApplicationError.None;
                OpenVR.Applications.GetApplicationPropertyString(ApplicationKey, EVRApplicationProperty.WorkingDirectory_String, oldWd, 260, ref wderror);
                if (wderror == EVRApplicationError.None)
                {
                    string manifestPath = oldWd.ToString();
                    manifestPath += "\\manifest.vrmanifest";
                    OpenVR.Applications.RemoveApplicationManifest(manifestPath);
                }
            }
            
            var manifesterr = OpenVR.Applications.AddApplicationManifest(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "manifest.vrmanifest"), false);
            
            OpenVR.Applications.SetApplicationAutoLaunch(ApplicationKey, true);
        }
        void UnsetOpenVRAutoLaunch()
        {
            var initerr = EVRInitError.None;
            OVRSystem = OpenVR.Init(ref initerr, EVRApplicationType.VRApplication_Overlay);
            
            if (initerr != EVRInitError.None)
            {
                return;
            }
            
            if (OpenVR.Applications.IsApplicationInstalled(ApplicationKey))
            {
                StringBuilder oldWd = new StringBuilder();
                var wderror = EVRApplicationError.None;
                OpenVR.Applications.GetApplicationPropertyString(ApplicationKey, EVRApplicationProperty.WorkingDirectory_String, oldWd, 260, ref wderror);
                if (wderror == EVRApplicationError.None)
                {
                    string manifestPath = oldWd.ToString();
                    manifestPath += "\\manifest.vrmanifest";
                    OpenVR.Applications.RemoveApplicationManifest(manifestPath);
                }
            }
            
            var manifesterr = OpenVR.Applications.AddApplicationManifest(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "manifest.vrmanifest"), false);
            
            OpenVR.Applications.SetApplicationAutoLaunch(ApplicationKey, false);
        }

        private void AppExit_Click(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void LoadConfig_Click(object sender, RoutedEventArgs e)
        {
            LoadConfig();
        }
        private void SaveConfig_Click(object sender, RoutedEventArgs e)
        {
            SaveConfig();
        }
        private void ConnectToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleListener();
        }
        private void SenderTargetList_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            RefreshSender();
        }
        private void SetSteamVRAutoLaunch_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This feature only works if you have SteamVR installed and running right now. Proceed?", "OSCRelay", MessageBoxButton.OK, MessageBoxImage.Information);

            try
            {
                SetOpenVRAutoLaunch();
                UseOpenVR = true;
                SaveConfig();
                LoadConfig();
            } catch (Exception ex)
            {

            }
        }
        private void UnsetSteamVRAutoLaunch_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This feature only works if you have SteamVR installed and running right now. Proceed?", "OSCRelay", MessageBoxButton.OK, MessageBoxImage.Information);

            try
            {
                UnsetOpenVRAutoLaunch();
                UseOpenVR = false;
                SaveConfig();
                LoadConfig();
            }
            catch (Exception ex)
            {

            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            OSCRelay.About AboutWindow = new OSCRelay.About();

            AboutWindow.ShowDialog();
        }
    }
}
