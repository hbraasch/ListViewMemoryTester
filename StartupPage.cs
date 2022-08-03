using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViewMemoryTester
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    internal class StartupPage : ContentPage, INotifyPropertyChanged
    {
        public class DisplayItem
        {
            public string Name { get; set; }
        }
        public List<DisplayItem> DisplayItems { get; set; } = new();

        ListView listView;
        public StartupPage()
        {

            BindingContext = this;

            listView = new ListView {
                SeparatorVisibility = SeparatorVisibility.None,
                SelectionMode = ListViewSelectionMode.Single
            };

            listView.ItemTemplate = new DataTemplate(()=> { 
            
                var label = new Label();
                label.SetBinding(Label.TextProperty, new Binding(nameof(DisplayItem.Name), BindingMode.OneWay));
                return new ViewCell { View = label };            
            });

            // listView.SetBinding(ListView.ItemsSourceProperty, new Binding(nameof(DisplayItems), BindingMode.OneWay));
            listView.ItemsSource = GenerateDisplayItems();

            Content = new VerticalStackLayout { Children = { listView } };

            ToolbarItems.Add(new ToolbarItem("Refresh", "", () => {
                listView.ItemsSource = GenerateDisplayItems();
                // DisplayItems = GenerateDisplayItems();
             }));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DisplayItems = GenerateDisplayItems();
        }

        public List<DisplayItem> GenerateDisplayItems()
        {
            var list = new List<DisplayItem>();
            for (int i = 0; i < 100; i++)
            {
                list.Add(new DisplayItem { Name = $"Item {i}" });
            }
            return list;
        }
    }


    public class StartupPageModel
    {

    }
}
