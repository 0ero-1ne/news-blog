   M S B A M L   `   `   `  ����D  @news-blog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:news_blog.View.Frontendnews_blog.View.Frontend  5   	   K*clr-namespace:news_blog.ViewModel.Frontendnews_blog.ViewModel.Frontend  5   	   %clr-namespace:news_blog	news_blog  5   	       news_blog.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?frontendViews%clr-namespace:news_blog.View.Frontend     5   	   IfrontendViewModels*clr-namespace:news_blog.ViewModel.Frontend     5   	   )localclr-namespace:news_blog     5   	     � Title$-  'News Blog - Новостной блог��5
   	   $	��700��6,   $
��1200��69    � Icon$, &pack://application:,,,/Images/Icon.png��5   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      .+�5   
   �� $��0��5      
���   �._�5      � .+�5      ��    LogoText#���   5      $��0��5      $��	NEWS BLOG��5      $
��Left=�5      $��Center=�5      $��15,0,0,0q�5       J� InputBindings 5      d� 5       d� MouseAction$ 	LeftClicke�6H    �� Command 6'   �� *OpenMainPage+6H   5       �� 5!       �� Orientation$ 
Horizontal=�5"      $
��Auto��5#      $��Center=�5$      $��Right=�5%      $��	0,10,0,10q�5&      .+�5'      ��   MenuItem#���  5(      $��Center=�5)      $!��Обратная связь��5*       5+      d� 5,      $ 	LeftClicke�6P    6+   �� *OpenFeedbackPage+6P   5.      �� 5/      $��40��50      $��40��51      $��0,0,15,0q�52       � Cursor$
 Hand��53      
������ $��0q�55        NoMouseOverButtonTemplate#���  56      ��57      �� *OpenProfileLoginPage+.��58      �� $!��../../../Images/profile.png��59      5:      5;      5<      5=      � 5>   
   $��1��6   	 � 	Resources	 5?      
 �� DataType-   'news_blog.ViewModel.Frontend.MainPageVM&    news_blog.View.Frontend.MainPage.   (news_blog.ViewModel.Frontend.LoginPageVM'   !news_blog.View.Frontend.LoginPage1   +news_blog.ViewModel.Frontend.RegisterPageVM*   $news_blog.View.Frontend.RegisterPage0  *news_blog.ViewModel.Frontend.ArticlePageVM)   #news_blog.View.Frontend.ArticlePage1	   +news_blog.ViewModel.Frontend.FeedbackPageVM*
   $news_blog.View.Frontend.FeedbackPage0   *news_blog.ViewModel.Frontend.ProfilePageVM)   #news_blog.View.Frontend.ProfilePage6   0news_blog.ViewModel.Frontend.CreateArticlePageVM/   )news_blog.View.Frontend.CreateArticlePage4   .news_blog.ViewModel.Frontend.EditArticlePageVM-   'news_blog.View.Frontend.EditArticlePage��%X  (��       5@      *, +)(�� 2     5C      *, +)(�� d     5F      *, +)(�� �     5I      *, +)(�� �     5L      *,	 +)(�� �     5O      *, +)(�� ,    5R      *, +)(�� ^    5U      *, +)�� 5@      "
  6   .R�5A        5B      �� 5C      "
  6   .R�5D        5E      �� 5F      "
  6   .R�5G        5H      �� 5I      "
  6   .R�5J        5K      �� 5L      "
 	 6   .R�5M      
  5N      �� 5O      "
  6   .R�5P        5Q      �� 5R      "
  6   .R�5S        5T      �� 5U      "
  6   .R�5V        5W      5X      .+�5Y      �� ��6   �� *CurrentViewModel+5Z      5[      5\      