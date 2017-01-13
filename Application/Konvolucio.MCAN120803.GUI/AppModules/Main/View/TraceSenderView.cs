namespace Konvolucio.MCAN120803.GUI.AppModules.Main.View
{
    using System;
    using System.Runtime.Remoting.Messaging;
    using System.Windows.Forms;
    using Properties;
    using WinForms.Framework;
    using Tracer.View;
    using View;

    public interface ITraceSenderView: IUiLayoutRestoring
    {
        ITraceGridView TraceGridView { get; }
        MultiPageCollection Pages { get; }
    }

    public partial class TraceSenderView : UserControl, ITraceSenderView
    {
        public ITraceGridView TraceGridView
        {
            get { return _traceGridView1; }
        }

        public MultiPageCollection Pages
        {
            get { return knvMultiPageView1.Pages; }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public TraceSenderView()
        {
            InitializeComponent();
            TraceGridView.FullScreenChanged += TraceGridView_FullScreenChanged;
        }


        /// <summary>
        /// 
        /// </summary>
        private void TraceGridView_FullScreenChanged(object sender, EventArgs e)
        {
            if (TraceGridView.IsFullscreen)
            {
                /*Eltüntetni a Sendert és megjegyzi hogy a Splitter-e hol volt*/
                Settings.Default.splitContainerTraceSender_SplitterDistance = splitContainerTraceSender.SplitterPrecent; /*Elmenti a Splitter-t az ablakhoz viszonyított ARÁNYÁT, UnHide-kor majd ezt ltája*/
                splitContainerTraceSender.SplitterDistance = splitContainerTraceSender.Size.Height - /*_senderView1.HeaderHeight*/ 25; /*Ezzel eltünt... csak a fjeléce látszik*/
                splitContainerTraceSender.IsSplitterFixed = true; /*User nem tudja megfogni a Splittert*/
                splitContainerTraceSender.FixedPanel = FixedPanel.Panel2; /*Fixálni kell az egyik panelt, mivel a ablakkezelő módosítja az Splitter arányát!!!*/
            }
            else
            {
                splitContainerTraceSender.FixedPanel = FixedPanel.None; /*Modosíthatja az ablakezelő is az arányokat.*/
                splitContainerTraceSender.IsSplitterFixed = false;
                /*Vissza állítja a Spittert az arányból*/
                splitContainerTraceSender.SplitterPrecent = Settings.Default.splitContainerTraceSender_SplitterDistance;
            }
        }

        #region Layout Save & Restore
        /// <summary>
        /// Mensd a ennek a vezérlő állapotát
        /// </summary>
        public void LayoutSave()
        {
            Settings.Default.SenderPanelHide = TraceGridView.IsFullscreen;

            if (TraceGridView.IsFullscreen)
            {
                /*MessageSender Hide-olva van akkor nem kell elmenteni az értékét így betöltéskor, visszaállhat egy korábbi méretre*/
            }
            else
            {
                /*Látja a user menteni kell az ARÁNYT*/
                Settings.Default.splitContainerTraceSender_SplitterDistance = splitContainerTraceSender.SplitterPrecent;
            }
        }

        /// <summary>
        /// Állisd vissza ennek a vezélrlőnek az állapotát
        /// </summary>
        public void LayoutRestore()
        {

              TraceGridView.IsFullscreen = Settings.Default.SenderPanelHide;

            if (TraceGridView.IsFullscreen)
            {
                /*Ezzel eltünt... csak a fjeléce látszik*/
                splitContainerTraceSender.SplitterDistance = splitContainerTraceSender.Size.Height - /*_senderView1.HeaderHeight*/ 25;
                splitContainerTraceSender.IsSplitterFixed = true;
                splitContainerTraceSender.FixedPanel = FixedPanel.Panel2;
            }
            else
            {
                /*pozicionálja a Splittert ha látja a mentett értékből*/
                //splitContainerTraceSender.SplitterDistance = Settings.Default.splitContainerTraceSender_SplitterDistance;
                splitContainerTraceSender.SplitterPrecent = Settings.Default.splitContainerTraceSender_SplitterDistance;
                splitContainerTraceSender.IsSplitterFixed = false;
                splitContainerTraceSender.FixedPanel = FixedPanel.None;
            }
        }
        #endregion

    }
}
