using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shopping
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Product> products = new ObservableCollection<Product>();
        ObservableCollection<string> cart = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();

            products.Add(new Product { Name = "Смартфон", Quantity = 10 });
            products.Add(new Product { Name = "Ноутбук", Quantity = 3 });
            products.Add(new Product { Name = "Наушники", Quantity = 25 });
            products.Add(new Product { Name = "Монитор", Quantity = 5 });
            products.Add(new Product { Name = "Мышь беспроводная", Quantity = 40 });
            products.Add(new Product { Name = "Клавиатура", Quantity = 15 });
            products.Add(new Product { Name = "Повербанк", Quantity = 18 });

            RefreshProductsList();
        }
        
        private void RefreshProductsList()
        {
            Products.Items.Clear();
            foreach (var p in products)
            {
                if (p.Quantity > 0)
                    Products.Items.Add(p);
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Products.SelectedItem is not Product selected)
            {
                MessageBox.Show("Выберите товар из списка.");
                return;
            }

            var stockItem = products.First(p => p.Name == selected.Name);
            if (stockItem == null || stockItem.Quantity <= 0)
            {
                MessageBox.Show("Товар закончился.");
                RefreshProductsList();
                return;
            }

            stockItem.Quantity--;
            cart.Add(selected.Name);

            RefreshProductsList();
            Cart.Items.Add($"{selected.Name} (1 шт.)");
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            if (cart.Count == 0)
            {
                MessageBox.Show("Корзина пуста.");
                return;
            }

            var grouped = cart
                .GroupBy(x => x)
                .Select(g => $"{g.Key} — {g.Count()} шт.")
                .ToList();

            string summary = "Ваш заказ:\n\n" + string.Join("\n", grouped);
            MessageBox.Show(summary, "Оформление заказа");

            cart.Clear();
            Cart.Items.Clear();
        }
    }

}
