using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using HarmonyLib;
using Lib;
using System.Drawing.Imaging;
using SolarisTheme.Properties;

namespace SolarisTheme
{
    public class SolarisTheme : AuroraPatch.Patch
    {
        public override string Description => "Solaris Theme";
        public override IEnumerable<string> Dependencies => new[] { "ThemeCreator", "Lib" };

        private static Lib.Lib lib;

        // Fonts
        private static readonly Font mainFont = new Font("Tahoma", 8.25f, FontStyle.Regular);
        private static readonly Font buttonFont = new Font("Tahoma", 7, FontStyle.Bold);

        // Colors
        private static readonly Color mainBackgroundColor = Color.FromArgb(12, 12, 12);
        private static readonly Color mainTextColor = Color.FromArgb(210, 210, 210);
        private static readonly Color buttonBackgroundColor = Color.FromArgb(23, 26, 39);
        private static readonly Color planetColor = Color.FromArgb(128, 128, 128);
        private static readonly Color orbitColor = Color.FromArgb(127, planetColor);

        protected override void Loaded(Harmony harmony)
        {
            lib = GetDependency<Lib.Lib>("Lib");

            ThemeCreator.ThemeCreator.AddColorChange(Color.FromArgb(0, 0, 64), mainBackgroundColor);
            ThemeCreator.ThemeCreator.AddColorChange(Color.FromArgb(255, 255, 192), mainTextColor);

            // Buttons
            ThemeCreator.ThemeCreator.AddColorChange(
                typeof(Button),
                new ThemeCreator.ColorChange { BackgroundColor = buttonBackgroundColor }
            );

            ThemeCreator.ThemeCreator.AddFontChange(mainFont);
            ThemeCreator.ThemeCreator.AddFontChange(typeof(Button), buttonFont);

            ThemeCreator.ThemeCreator.SetCometTailColor(orbitColor);
            ThemeCreator.ThemeCreator.SetPlanetColor(planetColor);

            ThemeCreator.ThemeCreator.DrawEllipsePrefixAction((graphics, pen) =>
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Note that LimeGreen circles are used to mark colonies as well, not just orbits
                if (pen.Color == Color.LimeGreen)
                {
                    pen.Color = orbitColor;
                }
            });

            ThemeCreator.ThemeCreator.FillEllipsePrefixAction((graphics, brush) =>
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
            });

            ThemeCreator.ThemeCreator.DrawLinePrefixAction((graphics, pen) =>
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Movement tails
                // TODO: Hostiles
                if (pen.Color == Color.FromArgb(0, 206, 209) || pen.Color == Color.FromArgb(255, 255, 192))
                {
                    pen.Color = ControlPaint.Dark(pen.Color, 0.5f);
                }
                // Comet path (distance ruler also uses LimeGreen but has pen.Width > 1)
                else if (pen.Color == Color.LimeGreen && pen.Width == 1)
                {
                    pen.Color = orbitColor;
                }
            });

            ThemeCreator.ThemeCreator.DrawStringPrefixAction((graphics, s, font, brush) =>
            {
                if (brush.GetType() == typeof(SolidBrush))
                {
                    var solidBrush = brush as SolidBrush;

                    if (solidBrush.Color == Color.FromArgb(255, 255, 192))
                    {
                        // solidBrush.Color = mainTextColor;
                    }
                }
            });

            // Button images
            ChangeButtonImage(AuroraButton.ZoomIn, Resources.Icon_ZoomIn, mainTextColor);
            ChangeButtonImage(AuroraButton.ZoomOut, Resources.Icon_ZoomOut, mainTextColor);
            ChangeButtonImage(AuroraButton.Up, Resources.Icon_Up, mainTextColor);
            ChangeButtonImage(AuroraButton.Down, Resources.Icon_Down, mainTextColor);
            ChangeButtonImage(AuroraButton.Left, Resources.Icon_Left, mainTextColor);
            ChangeButtonImage(AuroraButton.Right, Resources.Icon_Right, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarColony, Resources.Icon_Colony, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarIndustry, Resources.Icon_Industry, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarMining, Resources.Icon_Mining, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarResearch, Resources.Icon_Research, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarWealth, Resources.Icon_Wealth, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarClass, Resources.Icon_Class, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarProject, Resources.Icon_Project, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarFleet, Resources.Icon_Fleet, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarMissileDesign, Resources.Icon_MissileDesign, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarTurrent, Resources.Icon_Turrent, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarGroundForces, Resources.Icon_GroundForces, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarCommanders, Resources.Icon_Commanders, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarMedals, Resources.Icon_Medals, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarRace, Resources.Icon_Race, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarSystem, Resources.Icon_System, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarGalactic, Resources.Icon_Galactic, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarHabitable, Resources.Icon_Galactic, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarComparison, Resources.Icon_Comparison, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarIntelligence, Resources.Icon_Intelligence, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarTechnology, Resources.Icon_Technology, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarSurvey, Resources.Icon_Survey, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarSector, Resources.Icon_Sector, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarEvents, Resources.Icon_Events, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarRefreshTactical, Resources.Icon_Refresh, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarRefreshGalactic, Resources.Icon_Refresh, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarSave, Resources.Icon_Save, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarGame, Resources.Icon_Game, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarGrid, Resources.Icon_Grid, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarUndo, Resources.Icon_Undo, mainTextColor);
            ChangeButtonImage(AuroraButton.ToolbarSavePositions, Resources.Icon_SavePositions, mainTextColor);

            // Hook into Aurora forms constructors for some more advanced overrides
            var formConstructorPostfix = new HarmonyMethod(GetType().GetMethod("FormConstructorPostfix", AccessTools.all));

            foreach (var form in AuroraAssembly.GetTypes().Where(t => typeof(Form).IsAssignableFrom(t)))
            {
                foreach (var ctor in form.GetConstructors())
                {
                    harmony.Patch(ctor, postfix: formConstructorPostfix);
                }
            }
        }

        private static void FormConstructorPostfix(Form __instance)
        {
            __instance.HandleCreated += (Object sender, EventArgs e) =>
            {
                IterateControls((Control)sender);
            };
        }

        private static void IterateControls(Control control)
        {
            ApplyChanges(control);

            foreach (Control childControl in control.Controls)
            {
                IterateControls(childControl);
            } 
        }

        private static void ApplyChanges(Control control)
        {

            if (control.GetType() == typeof(TabControl))
            {
                ApplyTabControlChanges(control as TabControl);
            }
            else if (control.GetType() == typeof(Button))
            {
                ApplyButtonChanges(control as Button);
            }
            else if (control.GetType() == typeof(ComboBox))
            {
                ApplyComboBoxChanges(control as ComboBox);
            }
            else if (control.GetType() == typeof(TreeView))
            {
                ApplyTreeViewChanges(control as TreeView);
            }
            else if (control.GetType() == typeof(ListView))
            {
                ApplyListViewChanges(control as ListView);
            }
            else if (control.GetType() == typeof(ListBox))
            {
                ApplyListBoxChanges(control as ListBox);
            }
            else if (control.GetType() == typeof(FlowLayoutPanel))
            {
                ApplyFlowLayoutPanelChanges(control as FlowLayoutPanel);
            }
            else if (control.GetType() == typeof(Label))
            {
                ApplyLabelChanges(control as Label);
            }
        }

        private static void ApplyTabControlChanges(TabControl tabControl)
        {
            tabControl.SizeMode = TabSizeMode.FillToRight;

            // Patch tactical map tabs to fit on two lines (necessary due to custom font)
            if (tabControl.Name == "tabSidebar")
            {
                tabControl.Padding = new Point(5, 3);
            } 
        }

        private static void ApplyButtonChanges(Button button)
        {
            button.BackgroundImageLayout = ImageLayout.Center;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = mainBackgroundColor;
            button.FlatAppearance.BorderSize = 2;

            // With some exceptions just enable auto size for buttons (necessary due to custom font)
            if (button.Name != lib.KnowledgeBase.GetButtonName(AuroraButton.SubPulse)
                && button.Name != lib.KnowledgeBase.GetButtonName(AuroraButton.Increment))
            {
                button.AutoSize = true;
            }
        }

        private static void ApplyComboBoxChanges(ComboBox comboBox)
        {
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private static void ApplyTreeViewChanges(TreeView treeView)
        {
            treeView.BorderStyle = BorderStyle.None;
        }

        private static void ApplyListViewChanges(ListView listView)
        {
            if (listView.BorderStyle == BorderStyle.Fixed3D)
            {
                listView.BorderStyle = BorderStyle.FixedSingle;
            }

            if (listView.Name == "lstvSB")
            {
                listView.OwnerDraw = false;

                listView.Invalidated += (Object sender, InvalidateEventArgs e) =>
                {
                    foreach (ListViewItem item in ((ListView)sender).Items)
                    {
                        if (item.ForeColor == Color.FromArgb(255, 255, 192))
                        {
                            item.ForeColor = mainTextColor;
                        }

                        foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                        {
                            if (subItem.ForeColor == Color.FromArgb(255, 255, 192))
                            {
                                subItem.ForeColor = mainTextColor;
                            }
                        }
                    }
                };
            }
        }

        private static void ApplyListBoxChanges(ListBox listBox)
        {
            if (listBox.BorderStyle == BorderStyle.Fixed3D)
            {
                listBox.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private static void ApplyFlowLayoutPanelChanges(FlowLayoutPanel flowLayoutPanel)
        {
            if (flowLayoutPanel.BorderStyle == BorderStyle.Fixed3D)
            {
                flowLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private static void ApplyLabelChanges(Label label)
        {
            // Fix mass driver label overflow on top of combo box issue
            if (label.Name == "label17" && label.Text == "Mass Driver Destination")
            {
                label.Location = new Point(label.Location.X - 10, label.Location.Y);
            }
        }

        private static void ChangeButtonImage(AuroraButton button, Bitmap image, Color color)
        {
            Bitmap colorizedImage = ColorizeImage(image, color);

            ThemeCreator.ThemeCreator.AddImageChange(button, colorizedImage);
        }

        private static Bitmap ColorizeImage(Bitmap image, Color color)
        {
            var imageAttributes = new ImageAttributes();

            float r = color.R / 255f;
            float g = color.G / 255f;
            float b = color.B / 255f;

            float[][] colorMatrixElements = {
               new float[] {0, 0, 0, 0, 0},
               new float[] {0, 0, 0, 0, 0},
               new float[] {0, 0, 0, 0, 0},
               new float[] {0, 0, 0, 1, 0},
               new float[] {r, g, b, 0, 1}
            };

            var colorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(colorMatrix);

            var colorizedImage = new Bitmap(image.Width, image.Height);
            var graphics = Graphics.FromImage(colorizedImage);
            var rect = new Rectangle(0, 0, image.Width, image.Height);

            graphics.DrawImage(image, rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);

            return colorizedImage;
        }
    }
}
