   M S B A M L   `   `   `  ����  OSCRelay#    harunadev.OSCRelay.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   M IModernWpf.Controls, Version=0.9.6.0, Culture=neutral, PublicKeyToken=nullD @ModernWpf, Version=0.9.6.0, Culture=neutral, PublicKeyToken=null,ui!http://schemas.modernwpf.com/2019   5   	   0  *ModernWpf.Controls.Primitives.WindowHelper    UseModernWindowStyle  .  � Title$ OSCRelay��5	   	   $	��360��6   $	��560��6*   $	��380��69   $	��580��6F   .��5
      ]� .+�5   
   o� $
��Auto��6   $
��Auto��6   $	��Top=�6*   .@�5      m� -   5        $��	Menu_File��5      $��File(_F)��6(   .@�5      m� -   5         m�Name$ Menu_File_ReloadConfig��5      $��Reload Config (_C)��6;   $
��Left=�6W   $	��220��6r   6~   m� 5      -   5        $ Menu_File_AppExit��5      $��	Exit (_X)��66   $
��Left=�6I   $	��220��6d   6p   5      m� 5      -   5        $��	Menu_Help��5      $��Help(_H)��6)   .@�5      m� -   5        $ Menu_Help_HowTo��5      $��How To Use (_H)��64   $
��Left=�6M   $	��240��6h   6t   m� 5      -   5        $ Menu_Help_About��5      $��About OSCRelay (_A)��64   $
��Left=�6Q   $	��240��6l   6x   5      5      �� 5   
   $��Bottom=�6   .@�5      �� .��5      �� -   5         ��Name$ 
StatusText��5      5      5      � 5   
    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    .+�5!      � $��0��6   $��
12,12,12,6q�6     � ColumnDefinitions 5"      �� 5#      $
��Auto �6'   �� 5$      $
��Auto �6'   �� 5%      $��* �6'   �� 5&      $
��Auto �6'   .+�5)      �� $*��$/Icons/application-import-custom.png��6   $��Center=�6F   $��24��6a   $��24��6l   $��0��6x   $��0,0,12,0q�6�   �� 5*      $��Listener��6   $��Center=�6,   $��1��6G   $	��5,0q�6W   �� 5+      -   5        $��ConnectStatusText��5+      $#��Listening from 127.0.0.1:9001��65   $��Center=�6Z   $��2��6u   $	��5,0q�6�   
�������$��10(�6�   $
��Wrap=�6�   d� 5,      -	   5        $��ConnectToggleButton��5,      $��	Connected��6\   $��Center=�6p   $��3��6�   $	��5,0q�6�    d� 	IsChecked$ FalseY�6�   5-      � 50      $��1��6   $��	12,6,12,0q�6     51      �� 52      $
��Auto �6'   �� 53      $
��Auto �6'   �� 54      $��* �6'   �� 55      $
��Auto �6'   .+�58      �� $*��$/Icons/application-export-custom.png��6   $��Center=�6F   $��24��6a   $��24��6l   $��0��6x   $��0,0,12,0q�6�   �� 59      $��Sender��6   $��Center=�6*   $��1��6E   $	��5,0q�6U   � 5:      $��1��6   $��2��6$    5;      �� 5<      $
��Auto �6(   �� 5=      $��* �6(    5@      �� 5A      $��* �6+   �� 5B      $
��Auto �6+   .+�5E      �� -
   5        $%��SenderTargetListParseResultText��5E       $��Sender��6G   $��Center=�6U   $��0��6p   $	��5,0q�6}   
�������$��10(�6�   $
��Wrap=�6�   �� 5F      -   5        $��SenderTargetList��5F      $��1��6a   $��6q�6n    �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�6�   .��6�   ��5G      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5H   }   �� 6   2sys&clr-namespace:System;assembly=mscorlib   6}   	 �� 
TargetType"	 ��6$   
 �� BasedOn
 6B   .  (ModernWpf.Markup.StaticResourceExtension   �� ResourceKey" �� �� 	Resources 5I   "      CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch �� 	Transform T� X T� Y !� Visual��%  &	        !� 5J   &   $
 Left=�6I   $	 Top=�6[   $
 None=�6l    5K   *   V� 5L   .   .~�5M   2   �� �� 5N   2   �� 5O   2   T� 5P   2   $ 8Q�6E   $ 4Q�6K   5Q   /    5S   *   �� 5T   .   $��Example: 127.0.0.1:9002��64   
�������6V   5V   '   5W   #    �� Triggers 5X   "   P� 5Y   &    P� Property � �� �� Empty P� Value# Z 6>   . �5Z   *   ��  �� Property � �� �� Value# [  6G   5[   '   P� 5\   &    � �� 6>   Z� . �5]   *   ��  � ��# [  6G   5^   '   P� 5_   &    �@J�IsKeyboardFocused . . �5`   *   ��  � ��
 ���� 6G   �� 5a   *    � ��
 �����6G   �� 5b   *    �@��
CaretBrush
 �����6G   5c   '   5e      .��5f   )   5g      �� 5h      -   5        $$��SenderTargetListParseErrorText��5h       $	��218��6F   $��Sender��6U   $��1��6c   $��2��6p   $	��5,0q�6�   
�������$��10(�6�   $
��Wrap=�6�   5j      5k      5l      5m      5n      