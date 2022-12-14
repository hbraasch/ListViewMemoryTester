using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViewMemoryTester
{
    internal class StartupPage : ContentPage
    {
        public class DisplayItem
        {
            public string Name { get; set; }
        }

        ListView listView;
        public StartupPage()
        {
            var button = new Button { Text = "Click to launch [CollectionView] page"};
            button.Clicked += (s, e) => {

                Navigation.PushAsync(new CollectionViewPage());
            
            };

            listView = new ListView {
                SeparatorVisibility = SeparatorVisibility.None,
                SelectionMode = ListViewSelectionMode.Single
            };

            listView.ItemTemplate = new DataTemplate(()=> { 
            
                var label = new Label();
                label.SetBinding(Label.TextProperty, new Binding(nameof(DisplayItem.Name), BindingMode.OneWay));
                return new ViewCell { View = label };            
            });

            listView.ItemsSource = GenerateDisplayItems();

            Content = new VerticalStackLayout { Children = { button, listView }, Margin = 10 };

            ToolbarItems.Add(new ToolbarItem("Refresh", "ic_refresh.png", () => {
                listView.ItemsSource = GenerateDisplayItems();
            }));
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

}
