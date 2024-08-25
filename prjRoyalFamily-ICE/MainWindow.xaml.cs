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

namespace prjRoyalFamily_ICE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Tree familyTree;
        private RoyalFamily selectedNode;
        public MainWindow()
        {
            InitializeComponent();
            familyTree = new Tree();
            DataContext = familyTree;
            PopulateTreeView();
        }
        private void PopulateTreeView()
        {
            FamilyTreeView.Items.Clear();
            FamilyTreeView.Items.Add(CreateTreeViewItem(familyTree.Root));
        }

        private TreeViewItem CreateTreeViewItem(RoyalFamily node)
        {
            var item = new TreeViewItem
            {
                Header = node.Member.Name,
                Tag = node
            };
            foreach (var child in node.Children)
            {
                item.Items.Add(CreateTreeViewItem(child));
            }
            return item;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string searchName = SearchTextBox.Text;
            var memberNode = familyTree.FindMember(familyTree.Root, searchName);

            if (memberNode != null)
            {
                int position = familyTree.GetPositionInLine(memberNode);
                ResultLabel.Text = $"Found: {memberNode.Member.Name} - Position in line: {position}";
            }
            else
            {
                ResultLabel.Text = "Member not found.";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (selectedNode != null)
            {
                var newMember = new RoyalFamilyMember(
                    NameTextBox.Text,
                    DobPicker.SelectedDate ?? DateTime.Now,
                    IsAliveCheckBox.IsChecked ?? false
                );

                familyTree.Root.AddChild(selectedNode);
                PopulateTreeView();
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Please select a parent node to add a new member.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ClearInputFields()
        {
            NameTextBox.Clear();
            DobPicker.SelectedDate = DateTime.Now;
            IsAliveCheckBox.IsChecked = false;
        }

        private void FamilyTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (FamilyTreeView.SelectedItem != null)
            {
                selectedNode = (RoyalFamily)((TreeViewItem)FamilyTreeView.SelectedItem).Tag;
            }
        }
    }
}