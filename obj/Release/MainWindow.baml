   M S B A M L   `   `   `  ����   OSCRelay#    harunadev.OSCRelay.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     M IModernWpf.Controls, Version=0.9.6.0, Culture=neutral, PublicKeyToken=nullD @ModernWpf, Version=0.9.6.0, Culture=neutral, PublicKeyToken=null,ui!http://schemas.modernwpf.com/2019   0  *ModernWpf.Controls.Primitives.WindowHelper    UseModernWindowStyle  .  � Title$ OSCRelay by harunadev��$	��360��$	��560��$	��380��$	��580��.��]� .+�o� $
��Auto��$
��Auto��$	��Top=�.@�m� -   $��	Menu_File��$��File(_F)��.@�m� -    m�Name$ Menu_File_LoadConfig��$��Load Config (_L)��$
��Left=�$	��240��m� -   $ Menu_File_SaveConfig��$��Save Config (_S)��$
��Left=�$	��240��m� -   $$ Menu_File_SetSteamVRAutoLaunch��$"��Set SteamVR Auto Launch (_A)��$
��Left=�$	��240��m� -   $&  Menu_File_UnsetSteamVRAutoLaunch��$$��Unset SteamVR Auto Launch (_U)��$
��Left=�$	��240��m� -   $ Menu_File_AppExit��$��	Exit (_X)��$
��Left=�$	��240��m� -   $��	Menu_Help��$��Help(_H)��.@�m� -   $ Menu_Help_About��$��About OSCRelay (_A)��$
��Left=�$	��240���� $��Bottom=�.@��� .���� -	    ��Name$ 
StatusText���  � RowDefinitions �� $
��Auto ��� $��* �.+�� $��0��$��
12,12,12,6q� � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $*��$/Icons/application-import-custom.png��$��Center=�$��24��$��24��$��0��$��0,0,12,0q��� $��Listener��$��Center=�$��1��$	��5,0q��� -
   $��ConnectStatusText��$#��Listening from 127.0.0.1:9001��$��Center=�$��2��$	��5,0q�
�������$��10(�$
��Wrap=�d� -   $��ConnectToggleButton��$��	Connected��$��Center=�$��3��$	��5,0q� d� 	IsChecked$ FalseY�� $��1��$��	12,6,12,0q� �� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto �.+��� $*��$/Icons/application-export-custom.png��$��Center=�$��24��$��24��$��0��$��0,0,12,0q��� $��Sender��$��Center=�$��1��$	��5,0q�� $��1��$��2�� �� $
��Auto ��� $��* � �� $��* ��� $
��Auto �.+��� -   $%��SenderTargetListParseResultText��$��Sender��$��Center=�$��0��$	��5,0q�
�������$��10(�$
��Wrap=��� -   $��SenderTargetList��$��1��$��6q� �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�.����O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem �� 2sys&clr-namespace:System;assembly=mscorlib   	 �� 
TargetType"	 ��
 �� BasedOn
 .  (ModernWpf.Markup.StaticResourceExtension   �� ResourceKey" �� �� 	Resources    CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch �� 	Transform T� X T� Y !� Visual��%�   &	        !� $
 Left=�$	 Top=�$
 None=� V� .~��� �� �� T� $ 8Q�$ 4Q� �� $��Example: 127.0.0.1:9002��
������� �� Triggers P�  P� Property � �� �� Empty P� Value# Z . ���  �� Property � �� �� Value# [  P�  � �� Z� . ���  � ��# [  P�  �@J�IsKeyboardFocused . . ���  � ��
 ���� ��  � ��
 �������  �@��
CaretBrush
 �����.���� -   $$��SenderTargetListParseErrorText��$	��218��$��Sender��$��1��$��2��$	��5,0q�
�������$��10(�$
��Wrap=�