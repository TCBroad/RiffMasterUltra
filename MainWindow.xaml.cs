namespace RiffMasterUltra
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NoteSetViewModel viewModel = new();

        private readonly Button regenerateButton = new()
        {
            Content = "Regenerate",
            HorizontalAlignment = HorizontalAlignment.Right
        };

        public MainWindow()
        {
            this.InitializeComponent();

            this.DataContext = this.viewModel;
            this.regenerateButton.Click += this.Regenerate;
            Grid.SetColumn(this.regenerateButton, 14);
            
            this.GenerateLabels();
        }

        private void GenerateLabels()
        {
            this.NoteList.Children.Clear();

            for (var index = 0; index < this.viewModel.Notes.Count; index++)
            {
                var note = this.viewModel.Notes[index];
                var label = new Label
                {
                    Content = note.ToString(),
                    FontSize = 25,
                    Width = 45,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                };

                var border = new Border
                {
                    Child = label,
                    BorderThickness = new Thickness(1),
                    BorderBrush = new SolidColorBrush(Colors.Black),
                    Margin = new Thickness(2.5, 0, 2.5 ,0)
                };
                
                Grid.SetColumn(border, index);
                this.NoteList.Children.Add(border);
            }

            this.NoteList.Children.Add(this.regenerateButton);
        }

        private void GenerateGroupLabels()
        {
            if (this.GroupList == null)
            {
                return; 
            }
            
            this.GroupList.Children.Clear();

            foreach (var grouping in this.viewModel.Groupings)
            {
                var panel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5)};
                foreach (var note in grouping)
                {
                    var label = new Label
                    {
                        Content = note.ToString(),
                        FontSize = 15,
                        Width = 30,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                    };

                    var border = new Border
                    {
                        Child = label,
                        BorderThickness = new Thickness(1),
                        BorderBrush = new SolidColorBrush(Colors.Black),
                        Margin = new Thickness(1, 1, 1 ,1)
                    };
                
                    //Grid.SetColumn(border, index);
                    panel.Children.Add(border);
                }
                
                this.GroupList.Children.Add(panel);
            }
        }
        
        private void GeneratePatternLabels()
        {
            if (this.PatternList == null)
            {
                return; 
            }
            
            this.PatternList.Children.Clear();
            this.PatternHeading.Visibility = this.viewModel.Patterns.Any() ? Visibility.Visible : Visibility.Hidden;

            foreach (var grouping in this.viewModel.Patterns)
            {
                var panel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(5)};
                foreach (var note in grouping)
                {
                    var label = new Label
                    {
                        Content = note.ToString(),
                        FontSize = 15,
                        Width = 30,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                    };

                    var border = new Border
                    {
                        Child = label,
                        BorderThickness = new Thickness(1),
                        BorderBrush = new SolidColorBrush(Colors.Black),
                        Margin = new Thickness(1, 1, 1 ,1)
                    };
                
                    //Grid.SetColumn(border, index);
                    panel.Children.Add(border);
                }
                
                this.PatternList.Children.Add(panel);
            }
        }

        private void Regenerate(object sender, RoutedEventArgs e)
        {
            this.viewModel.GenerateNoteSet();
            
            this.GenerateLabels();
            
            this.UpdateGroups(sender, null);
        }

        private void UpdateGroups(object sender, SelectionChangedEventArgs? e)
        {
            this.viewModel.GenerateGroupings();
            this.viewModel.GeneratePatterns();
            
            this.GenerateGroupLabels();
            this.GeneratePatternLabels();
        }
    }
}