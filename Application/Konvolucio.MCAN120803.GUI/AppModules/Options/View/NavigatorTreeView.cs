

using Konvolucio.WinForms.Framework;

namespace Konvolucio.MCAN120803.GUI.AppModules.Options.View
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    public partial class NavigatorTreeView : TreeView
    {
        [Category("KNV Option Item")]
        [DisplayName("[1]Genaral")]
        [SettingsBindable(true)]
        [Localizable(true)]
        public string GeneralNode
        {
            get { return _generalNode.Text; }
            set { _generalNode.Text = value; }
        }
        readonly TreeNode _generalNode;

        [Category("KNV Option Item")]
        [DisplayName("[2]Project")]
        [SettingsBindable(true)]
        [Localizable(true)]
        public string ProjectNode
        {
            get { return _projectNode.Text; }
            set { _projectNode.Text = value; }
        }
        readonly TreeNode _projectNode;

        [Category("KNV Option Items")]
        [DisplayName("[2.1]Project Representation")]
        [SettingsBindable(true)]
        [Localizable(true)]
        public string ProjectRepresentationNode
        {
            get { return _projectRepresentationNode.Text; }
            set { _projectRepresentationNode.Text = value; }
        }
        readonly TreeNode _projectRepresentationNode;

        [Category("KNV Option Items")]
        [DisplayName("[2.2]Project Functions")]
        [SettingsBindable(true)]
        [Localizable(true)]
        public string ProjectFunctionsNode
        {
            get { return _projectFunctionsNode.Text; }
            set { _projectFunctionsNode.Text = value; }
        }
        readonly TreeNode _projectFunctionsNode;

        [Category("KNV Option Item")]
        [DisplayName("[2.3]Custom Arbitration Id Cells")]
        [SettingsBindable(true)]
        [Localizable(true)]
        public string ProjectCustomArbIdColumnsNode
        {
            get { return _projectCustomArbIdColumnsNode.Text; }
            set { _projectCustomArbIdColumnsNode.Text = value; }
        }
        readonly TreeNode _projectCustomArbIdColumnsNode;


        [Category("KNV Option Items")]      
        [DisplayName("[3]Developer")]
        [SettingsBindable(true)]
        [Localizable(true)]
        public string DeveloperNode
        {
            get { return _developerNode.Text; }
            set { _developerNode.Text = value; }
        }
        readonly TreeNode _developerNode;


        [Category("KNV Option Items")]
        [DisplayName("[4]About")]
        [SettingsBindable(true)]
        [Localizable(true)]
        public string AboutNode
        {
            get { return _aboutNode.Text; }
            set { _aboutNode.Text = value; }
        }
        readonly TreeNode _aboutNode;

        /*A Designernek ezzel nem szabad foglalkoznia*/
        [DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new TreeNodeCollection Nodes
        {
            get { return base.Nodes; }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public NavigatorTreeView()
        {
            InitializeComponent();

            if (Nodes.Count != 0)
                Nodes.Clear();

            _generalNode = new TreeNode{Name = PropertyPlus.GetPropertyName(() => GeneralNode)};


            _projectNode = new TreeNode {Name = PropertyPlus.GetPropertyName(() => ProjectNode)};
            _projectRepresentationNode = new TreeNode{Name = PropertyPlus.GetPropertyName(() => ProjectRepresentationNode)};
            _projectNode.Nodes.Add(_projectRepresentationNode);
            _projectFunctionsNode = new TreeNode{Name = PropertyPlus.GetPropertyName(() => ProjectFunctionsNode)};
            _projectNode.Nodes.Add(_projectFunctionsNode);
            _projectCustomArbIdColumnsNode = new TreeNode { Name = PropertyPlus.GetPropertyName(() => ProjectCustomArbIdColumnsNode) };
            _projectNode.Nodes.Add(_projectCustomArbIdColumnsNode);

            _developerNode = new TreeNode{Name = PropertyPlus.GetPropertyName(() => DeveloperNode)};

            _aboutNode = new TreeNode { Name = PropertyPlus.GetPropertyName(() => AboutNode) };

            Nodes.AddRange(new TreeNode[] { _generalNode, _projectNode, _developerNode, _aboutNode });

            SelectedNode = _generalNode;
        }
    }
}
