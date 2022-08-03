using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public List<DisplayItem> DisplayItems { get; set; } = new();

        ListView listView;
        public StartupPage()
        {

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

            Content = new VerticalStackLayout { Children = { listView } };

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
