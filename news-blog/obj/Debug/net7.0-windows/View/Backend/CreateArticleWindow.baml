   M S B A M L   `   `   `  ����D  @news-blog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:news_blog.View.Backendnews_blog.View.Backend  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   0    *news_blog.View.Backend.CreateArticleWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:news_blog.View.Backend     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5        $��CreateArticleWnd��5   	     � Title$)  #News Blog - Новая статья��5	   	   $	��750��6*   $	��500��67    � Icon$, &pack://application:,,,/Images/Icon.png��5
   	   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��70 �6   .+�5   
   �� $��0��5      .+�5      �� $��
10,10,10,0q�5      $��Заголовок��5         	FormLabel#���   5      �� 5      $��10q�5        
FormBorder#���  5      ._�5      ��   FormTextBox#���  5      ��5      �� *Title+5      �� 5      $��
10,10,10,0q�5      $-��'Приветственный текст��5       #���   5!      �� 5"      $��10q�5#      #���  5$      ._�5%      �� #���  5&      ��5'      �� *	ShortText+5(      �� 5)      $��
10,10,10,0q�5*      $��Текст статьи��5+      #���   5,      �� 5-      $��10q�5.      $	��100��5/      #���  50      ._�51      �� #���  52       �� TextWrapping$
 Wrap=�54       �� AcceptsReturn$
 True��55      ��53      �� *Text+55      56      �� 57      $��
10,10,10,0q�58      $��Категория��59      #���   5:      �� 5;        NewItemComboBox#���  5<       �� DisplayMemberPath$ Title��5>       �� MaxDropDownHeight$	 150��5@      ��5=      �� *
Categories+ �� SelectedItem 5?      �� *
Category+5A      �� 5B      $��
10,10,10,0q�5C      $��
Автор��5D      #���   5E      �� 5F      #���  5G      $ Username��5I      $	 150��5K      ��5H      �� *Users+ 5J      �� *Author+5L      �� 5M      $��
10,10,10,0q�5N      $��Изображение��5O      #���   5P      �� 5Q      $��1��5R      $��10q�5S      $	��150��5T      $
��Left=�5U      ._�5V      �� $��
Обзор��5W        NewItemButton#���  5X      $��5q�5Z      ��5Y      �� *
OpenDialog+5Z      5[      �� 5\      $��
10,0,10,10q�5^      ��5]      �� *	ImagePath+5^      5_      �� 5`   
   $��1��5a      $��10q�5b      ._�5c      �� $#��Добавить статью��5d      #���  5e      ��5f      �� *SaveArticle+��5g      ��  �� ElementName$ CreateArticleWnd��5h      5i      5j      