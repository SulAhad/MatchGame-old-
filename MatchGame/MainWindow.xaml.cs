using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();//Вызов метода
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()//Создает список из восьми пар эмодзи
             {
                "🐶","🐶",
                "🐺","🐺",
                "🦁","🦁",
                "🦒","🦒",
                "🐻","🐻",
                "🐹","🐹",
                "🐗","🐗",
                "🐮","🐮",
             };


            Random random = new Random();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())//Находит каждый элемент TextBlock в сетке и повторяет следующие команды для каждого элемента
            {
                int index = random.Next(animalEmoji.Count);//Выбирает случайное число от 0 до ко- личества эмодзи в списке и назначает ему имя «index»
                string nextEmoji = animalEmoji[index];//Использует случайное число с именем «index» для получения случайного эмодзи из списка
                textBlock.Text = nextEmoji;//Обновляет TextBlock случайным эмодзи из списка
                animalEmoji.RemoveAt(index);//Удаляет случайный эмодзи из списка
            }
        }

        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
