using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WeifenLuo.WinFormsUI.Docking
{
    internal class VS2012LightDockPaneCaption : DockPaneCaptionBase
    {
        private sealed class InertButton : InertButtonBase
        {
            private readonly Bitmap m_image;
            private readonly Bitmap m_imageAutoHide;

            public InertButton(VS2012LightDockPaneCaption dockPaneCaption, Bitmap image, Bitmap imageAutoHide)
            {
                m_dockPaneCaption = dockPaneCaption;
                m_image = image;
                m_imageAutoHide = imageAutoHide;
                RefreshChanges();
            }

            private readonly VS2012LightDockPaneCaption m_dockPaneCaption;

            private VS2012LightDockPaneCaption DockPaneCaption
            {
                get { return m_dockPaneCaption; }
            }

            private bool IsAutoHide
            {
                get { return DockPaneCaption.DockPane.IsAutoHide; }
            }

            public override Bitmap Image
            {
                get { return IsAutoHide ? m_imageAutoHide : m_image; }
            }

            protected override void OnRefreshChanges()
            {
                if (DockPaneCaption.DockPane.DockPanel != null)
                {
                    if (DockPaneCaption.TextColor != ForeColor)
                    {
                        ForeColor = DockPaneCaption.TextColor;
                        Invalidate();
                    }
                }
            }
        }

        #region consts

        private const int _TextGapTop = 3;
        private const int _TextGapBottom = 2;
        private const int _TextGapLeft = 2;
        private const int _TextGapRight = 3;
        private const int _ButtonGapTop = 2;
        private const int _ButtonGapBottom = 1;
        private const int _ButtonGapBetween = 1;
        private const int _ButtonGapLeft = 1;
        private const int _ButtonGapRight = 2;

        #endregion consts

        private static Bitmap _imageButtonClose;

        private static Bitmap ImageButtonClose
        {
            get { return _imageButtonClose ?? (_imageButtonClose = Resources.DockPane_Close); }
        }

        private InertButton m_buttonClose;

        private InertButton ButtonClose
        {
            get
            {
                if (m_buttonClose == null)
                {
                    m_buttonClose = new InertButton(this, ImageButtonClose, ImageButtonClose);
                    m_toolTip.SetToolTip(m_buttonClose, ToolTipClose);
                    m_buttonClose.Click += Close_Click;
                    Controls.Add(m_buttonClose);
                }

                return m_buttonClose;
            }
        }

        private static Bitmap _imageButtonAutoHide;

        private static Bitmap ImageButtonAutoHide
        {
            get { return _imageButtonAutoHide ?? (_imageButtonAutoHide = Resources.DockPane_AutoHide); }
        }

        private static Bitmap _imageButtonDock;

        private static Bitmap ImageButtonDock
        {
            get { return _imageButtonDock ?? (_imageButtonDock = Resources.DockPane_Dock); }
        }

        private InertButton m_buttonAutoHide;

        private InertButton ButtonAutoHide
        {
            get
            {
                if (m_buttonAutoHide == null)
                {
                    m_buttonAutoHide = new InertButton(this, ImageButtonDock, ImageButtonAutoHide);
                    m_toolTip.SetToolTip(m_buttonAutoHide, ToolTipAutoHide);
                    m_buttonAutoHide.Click += AutoHide_Click;
                    Controls.Add(m_buttonAutoHide);
                }

                return m_buttonAutoHide;
            }
        }

        private static Bitmap _imageButtonOptions;

        private static Bitmap ImageButtonOptions
        {
            get { return _imageButtonOptions ?? (_imageButtonOptions = Resources.DockPane_Option); }
        }

        private InertButton m_buttonOptions;

        private InertButton ButtonOptions
        {
            get
            {
                if (m_buttonOptions == null)
                {
                    m_buttonOptions = new InertButton(this, ImageButtonOptions, ImageButtonOptions);
                    m_toolTip.SetToolTip(m_buttonOptions, ToolTipOptions);
                    m_buttonOptions.Click += Options_Click;
                    Controls.Add(m_buttonOptions);
                }
                return m_buttonOptions;
            }
        }

        private readonly IContainer m_components;

        private IContainer Components
        {
            get { return m_components; }
        }

        private readonly ToolTip m_toolTip;

        public VS2012LightDockPaneCaption(DockPane pane)
            : base(pane)
        {
            SuspendLayout();

            m_components = new Container();
            m_toolTip = new ToolTip(Components);

            ResumeLayout();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Components.Dispose();
            base.Dispose(disposing);
        }

        private static int TextGapTop
        {
            get { return _TextGapTop; }
        }

        public Font TextFont
        {
            get { return DockPane.DockPanel.Skin.DockPaneStripSkin.TextFont; }
        }

        private static int TextGapBottom
        {
            get { return _TextGapBottom; }
        }

        private static int TextGapLeft
        {
            get { return _TextGapLeft; }
        }

        private static int TextGapRight
        {
            get { return _TextGapRight; }
        }

        private static int ButtonGapTop
        {
            get { return _ButtonGapTop; }
        }

        private static int ButtonGapBottom
        {
            get { return _ButtonGapBottom; }
        }

        private static int ButtonGapLeft
        {
            get { return _ButtonGapLeft; }
        }

        private static int ButtonGapRight
        {
            get { return _ButtonGapRight; }
        }

        private static int ButtonGapBetween
        {
            get { return _ButtonGapBetween; }
        }

        private static string _toolTipClose;

        private static string ToolTipClose
        {
            get { return _toolTipClose ?? (_toolTipClose = Strings.DockPaneCaption_ToolTipClose); }
        }

        private static string _toolTipOptions;

        private static string ToolTipOptions
        {
            get { return _toolTipOptions ?? (_toolTipOptions = Strings.DockPaneCaption_ToolTipOptions); }
        }

        private static string _toolTipAutoHide;

        private static string ToolTipAutoHide
        {
            get { return _toolTipAutoHide ?? (_toolTipAutoHide = Strings.DockPaneCaption_ToolTipAutoHide); }
        }

        private static Blend _activeBackColorGradientBlend;

        private static Blend ActiveBackColorGradientBlend
        {
            get
            {
                if (_activeBackColorGradientBlend == null)
                {
                    var blend = new Blend(2) {Factors = new[] {0.5F, 1.0F}, Positions = new[] {0.0F, 1.0F}};

                    _activeBackColorGradientBlend = blend;
                }

                return _activeBackColorGradientBlend;
            }
        }

        private Color TextColor
        {
            get
            {
                if (DockPane.IsActivated)
                    return DockPane.DockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.TextColor;
                return DockPane.DockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.TextColor;
            }
        }

        private const TextFormatFlags _textFormat = TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter;

        private TextFormatFlags TextFormat
        {
            get
            {
                if (RightToLeft == RightToLeft.No)
                    return _textFormat;
                return _textFormat | TextFormatFlags.RightToLeft | TextFormatFlags.Right;
            }
        }

        protected internal override int MeasureHeight()
        {
            var height = TextFont.Height + TextGapTop + TextGapBottom;

            if (height < ButtonClose.Image.Height + ButtonGapTop + ButtonGapBottom)
                height = ButtonClose.Image.Height + ButtonGapTop + ButtonGapBottom;

            return height;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawCaption(e.Graphics);
        }

        private void DrawCaption(Graphics g)
        {
            if (ClientRectangle.Width == 0 || ClientRectangle.Height == 0)
                return;

            var rect = ClientRectangle;

            var captionColor = DockPane.IsActivated ? DockPane.DockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.StartColor : DockPane.DockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.StartColor;

            var captionBrush = new SolidBrush(captionColor);
            g.FillRectangle(captionBrush, rect);

            var rectCaption = rect;

            var rectCaptionText = rectCaption;
            rectCaptionText.X += TextGapLeft;
            rectCaptionText.Width -= TextGapLeft + TextGapRight;
            rectCaptionText.Width -= ButtonGapLeft + ButtonClose.Width + ButtonGapRight;
            if (ShouldShowAutoHideButton)
                rectCaptionText.Width -= ButtonAutoHide.Width + ButtonGapBetween;
            if (HasTabPageContextMenu)
                rectCaptionText.Width -= ButtonOptions.Width + ButtonGapBetween;
            rectCaptionText.Y += TextGapTop;
            rectCaptionText.Height -= TextGapTop + TextGapBottom;

            Color colorText;
            if (DockPane.IsActivated)
                colorText = DockPane.DockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.TextColor;
            else
                colorText = DockPane.DockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.TextColor;

            TextRenderer.DrawText(g, DockPane.CaptionText, TextFont, DrawHelper.RtlTransform(this, rectCaptionText), colorText, TextFormat);

            var rectDotsStrip = rectCaptionText;
            var textLength = (int)g.MeasureString(DockPane.CaptionText, TextFont).Width + TextGapLeft;
            rectDotsStrip.X += textLength;
            rectDotsStrip.Width -= textLength;
            rectDotsStrip.Height = ClientRectangle.Height;

            Color dotsColor;
            if (DockPane.IsActivated)
                dotsColor = DockPane.DockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.ActiveCaptionGradient.EndColor;
            else
                dotsColor = DockPane.DockPanel.Skin.DockPaneStripSkin.ToolWindowGradient.InactiveCaptionGradient.EndColor;

            DrawDotsStrip(g, rectDotsStrip, dotsColor);
        }

        protected void DrawDotsStrip(Graphics g, Rectangle rectStrip, Color colorDots)
        {
            if (rectStrip.Width <= 0 || rectStrip.Height <= 0)
                return;

            var penDots = new Pen(colorDots, 1) {DashStyle = DashStyle.Custom, DashPattern = new float[] {1, 3}};
            var positionY = rectStrip.Height / 2;

            g.DrawLine(penDots, rectStrip.X + 2, positionY, rectStrip.X + rectStrip.Width - 2, positionY);

            g.DrawLine(penDots, rectStrip.X, positionY - 2, rectStrip.X + rectStrip.Width, positionY - 2);
            g.DrawLine(penDots, rectStrip.X, positionY + 2, rectStrip.X + rectStrip.Width, positionY + 2);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            SetButtonsPosition();
            base.OnLayout(levent);
        }

        protected override void OnRefreshChanges()
        {
            SetButtons();
            Invalidate();
        }

        private bool CloseButtonEnabled
        {
            get { return (DockPane.ActiveContent != null) && DockPane.ActiveContent.DockHandler.CloseButton; }
        }

        /// <summary>
        /// Determines whether the close button is visible on the content
        /// </summary>
        private bool CloseButtonVisible
        {
            get { return (DockPane.ActiveContent != null) && DockPane.ActiveContent.DockHandler.CloseButtonVisible; }
        }

        private bool ShouldShowAutoHideButton
        {
            get { return !DockPane.IsFloat; }
        }

        private void SetButtons()
        {
            ButtonClose.Enabled = CloseButtonEnabled;
            ButtonClose.Visible = CloseButtonVisible;
            ButtonAutoHide.Visible = ShouldShowAutoHideButton;
            ButtonOptions.Visible = HasTabPageContextMenu;
            ButtonClose.RefreshChanges();
            ButtonAutoHide.RefreshChanges();
            ButtonOptions.RefreshChanges();

            SetButtonsPosition();
        }

        private void SetButtonsPosition()
        {
            // set the size and location for close and auto-hide buttons
            var rectCaption = ClientRectangle;
            var buttonWidth = ButtonClose.Image.Width;
            var buttonHeight = ButtonClose.Image.Height;
            var height = rectCaption.Height - ButtonGapTop - ButtonGapBottom;
            if (buttonHeight < height)
            {
                buttonWidth = buttonWidth * (height / buttonHeight);
                buttonHeight = height;
            }
            var buttonSize = new Size(buttonWidth, buttonHeight);
            var x = rectCaption.X + rectCaption.Width - 1 - ButtonGapRight - m_buttonClose.Width;
            var y = rectCaption.Y + ButtonGapTop;
            var point = new Point(x, y);
            ButtonClose.Bounds = DrawHelper.RtlTransform(this, new Rectangle(point, buttonSize));

            // If the close button is not visible draw the auto hide button overtop.
            // Otherwise it is drawn to the left of the close button.
            if (CloseButtonVisible)
                point.Offset(-(buttonWidth + ButtonGapBetween), 0);

            ButtonAutoHide.Bounds = DrawHelper.RtlTransform(this, new Rectangle(point, buttonSize));
            if (ShouldShowAutoHideButton)
                point.Offset(-(buttonWidth + ButtonGapBetween), 0);
            ButtonOptions.Bounds = DrawHelper.RtlTransform(this, new Rectangle(point, buttonSize));
        }

        private void Close_Click(object sender, EventArgs e)
        {
            DockPane.CloseActiveContent();
        }

        private void AutoHide_Click(object sender, EventArgs e)
        {
            DockPane.DockState = DockHelper.ToggleAutoHideState(DockPane.DockState);
            if (DockHelper.IsDockStateAutoHide(DockPane.DockState))
            {
                DockPane.DockPanel.ActiveAutoHideContent = null;
                DockPane.NestedDockingStatus.NestedPanes.SwitchPaneWithFirstChild(DockPane);
            }
        }

        private void Options_Click(object sender, EventArgs e)
        {
            ShowTabPageContextMenu(PointToClient(MousePosition));
        }

        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);
            PerformLayout();
        }
    }
}