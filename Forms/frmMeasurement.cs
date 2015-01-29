using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeteoInfo.Forms
{
    public enum MeasureType
    {
        Length,
        Area,
        Feature
    }

    public partial class frmMeasurement : Form
    {
        #region Variables
        private MeasureType _measureType;
        private bool _isArea;
        private double _previousValue;
        private double _currentValue;
        private double _totalValue;
        private string _unitStr;

        #endregion

        #region Constructor
        public frmMeasurement()
        {
            InitializeComponent();
        }

        private void frmMeasurement_Load(object sender, EventArgs e)
        {
            TSCB_Units.Items.Clear();
            TSCB_Units.Items.Add("Meters");
            TSCB_Units.Items.Add("Kilometers");
            TSCB_Units.Text = "Kilometers";
            _unitStr = TSCB_Units.Text;

            TSB_Feature.PerformClick();
            _previousValue = 0;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Get Measure type
        /// </summary>
        public MeasureType MeasureType
        {
            get { return _measureType; }
        }

        /// <summary>
        /// Get or set if is area
        /// </summary>
        public bool IsArea
        {
            get { return _isArea; }
            set { _isArea = value; }
        }

        /// <summary>
        /// Get or set previous value
        /// </summary>
        public double PreviousValue
        {
            get { return _previousValue; }
            set { _previousValue = value; }
        }

        /// <summary>
        /// Get or set current value
        /// </summary>
        public double CurrentValue
        {
            get { return _currentValue; }
            set 
            {
                _currentValue = value;
                ShowValue();
            }
        }

        /// <summary>
        /// Get or set total value
        /// </summary>
        public double TotalValue
        {
            get { return _totalValue; }
            set { _totalValue = value; }
        }

        #endregion

        #region Methods
        private double ConvertValue(double aValue)
        {
            double bValue = aValue;
            switch (_unitStr)
            {
                case "Kilometers":
                    if (IsArea)
                        bValue = aValue / 1000000;
                    else
                        bValue = aValue / 1000;
                    break;
            }

            return bValue;
        }

        private void ShowValue()
        {
            string unitStr = _unitStr;
            string tStr = "Length";
            string lines = string.Empty;
            double currentValue = ConvertValue(_currentValue);

            if (_isArea)
            {
                unitStr = unitStr + "^2";
                lines = "Area: " + currentValue.ToString("#,###") + " " + unitStr;
            }
            else
            {
                lines = "Segement " + tStr + ": " + currentValue.ToString("#,###") + " " + unitStr;
                _totalValue = _previousValue + _currentValue;
                lines = lines + Environment.NewLine + "Total " + tStr + ": " + ConvertValue(_totalValue).ToString("#,###") +
                    " " + unitStr;
            }
            TB_content.Text = lines;
        }

        #endregion

        private void TSB_Distance_Click(object sender, EventArgs e)
        {
            TSB_Area.Checked = false;
            TSB_Feature.Checked = false;
            TSB_Distance.Checked = true;

            _measureType = MeasureType.Length;
            _isArea = false;
        }

        private void TSB_Area_Click(object sender, EventArgs e)
        {
            TSB_Area.Checked = true;
            TSB_Feature.Checked = false;
            TSB_Distance.Checked = false;

            _measureType = MeasureType.Area;
            _isArea = true;
        }

        private void frmMeasurement_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMain.CurrentWin.MapDocument.ActiveMapFrame.MapView.Refresh();
        }

        private void TSCB_Units_SelectedIndexChanged(object sender, EventArgs e)
        {
            _unitStr = TSCB_Units.Text;
        }

        private void TSB_Feature_Click(object sender, EventArgs e)
        {
            TSB_Feature.Checked = true;
            TSB_Distance.Checked = false;
            TSB_Area.Checked = false;

            _measureType = MeasureType.Feature;
        }
    }
}
