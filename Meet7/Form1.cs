namespace Meet7
{
    //Создайте редактор на базе RichTextBox и добавьте в него возможность выбора из каталога и просмотра содержимого файла перетаскиванием файла на поле редактора. 
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            richTextBox1.AllowDrop = true;
            richTextBox1.DragEnter += RichTextBox1_DragEnter;
            richTextBox1.DragDrop += RichTextBox1_DragDrop;
            treeView1.ItemDrag += TreeView1_ItemDrag;
            FillDriveNodes();
        }

        private void TreeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            (sender as TreeView).DoDragDrop((e.Item as TreeNode).FullPath, DragDropEffects.Copy | DragDropEffects.Move);
            richTextBox1.Text = File.ReadAllText((e.Item as TreeNode).FullPath);
        }

        private void FillDriveNodes()
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                TreeNode driveNode = new TreeNode(drive.Name);
                treeView1.Nodes.Add(driveNode);
            }
        }

        private void RichTextBox1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                richTextBox1.Text = File.ReadAllText(e.Data.GetData(DataFormats.StringFormat).ToString());
            }
            catch (Exception ex)
            {

            }
        }

        private void RichTextBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                e.Node.Nodes.Clear();
                foreach (string dir in Directory.GetDirectories(e.Node.FullPath))
                {
                    TreeNode driveNode = new TreeNode(new DirectoryInfo(dir).Name);

                    e.Node.Nodes.Add(driveNode);
                }
                foreach (string dir in Directory.GetFiles(e.Node.FullPath))
                {
                    TreeNode driveNode = new TreeNode(new DirectoryInfo(dir).Name);
                    e.Node.Nodes.Add(driveNode);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
