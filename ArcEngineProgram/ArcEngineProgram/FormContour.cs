
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using System.IO;
using ESRI.ArcGIS.DataSourcesFile;


namespace ArcEngineProgram
{
    struct Point3D
    {
       public string ID;
       public double X;
       public double Y;
       public double Z;
    }

    public partial class FormContour : Form
    {
        public FormContour()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "";
        }

        private List<Point3D> Read_ContourLineFile(string FileName)
        {
            FileStream fs = new FileStream(FileName, FileMode.Open);
            StreamReader sreader = new StreamReader(fs);
            List<Point3D> PointList=new List<Point3D>();
            while (!sreader.EndOfStream)
            {
                string line = sreader.ReadLine();
                string[] SplitArray = line.Split(',');
                if (SplitArray.Length != 0)
                {
                    Point3D pt3d;
                    pt3d.ID = SplitArray[0];
                    pt3d.X=Convert.ToDouble(SplitArray[1]);
                    pt3d.Y=Convert.ToDouble(SplitArray[2]);
                    pt3d.Z = Convert.ToDouble(SplitArray[3]);
                    PointList.Add(pt3d);
                }
            }
            return PointList;
        }

        private IFeatureLayer CreateSHP_Point(List<Point3D> PointList, string FileFullPath)
        {
            int index = FileFullPath.LastIndexOf("\\");
            string filename = System.IO.Path.GetFileName(FileFullPath);
            string shpFolder = System.IO.Path.GetDirectoryName(FileFullPath);
            IWorkspaceFactory shpWSF = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace shpFWS = (IFeatureWorkspace)shpWSF.OpenFromFile(FileFullPath.Substring(0,index), 0);
            //创建字段编辑所需要的接口
            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit;
            pFieldsEdit = (IFieldsEdit)pFields;
            //给字段属性、类型赋值
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = (IFieldEdit)pField;
            pFieldEdit.Name_2 = "Shape";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            IGeometryDef pGeometryDef = new GeometryDefClass();
            IGeometryDefEdit pGDefEdit = (IGeometryDefEdit)pGeometryDef;
            pGDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;

            pFieldsEdit.AddField(pField);
            pFieldEdit.GeometryDef_2 = pGeometryDef;

            IFeatureClass pFeatureClass;
            pFeatureClass = shpFWS.CreateFeatureClass(filename, pFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");

            IPoint pPoint = new PointClass();
            for (int j = 0; j < PointList.Count; j++)
            {
                pPoint.X = PointList[j].X;
                pPoint.Y = PointList[j].Y;

                IFeature pFeature = pFeatureClass.CreateFeature();
                pFeature.Shape = pPoint;
                pFeature.Store();
            }

            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            pFeatureLayer.Name = filename;
            pFeatureLayer.FeatureClass = pFeatureClass;
            return pFeatureLayer;
        }

        private void 原始测量文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "pnt文件(*.pnt)|*.pnt";
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            string fileFullPath = openFileDialog1.FileName;
            List<Point3D> ContourPointList = new List<Point3D>();
            toolStripStatusLabel1.Text = "正在读取pnt文件...";
            ContourPointList = Read_ContourLineFile(fileFullPath);
            toolStripStatusLabel1.Text = "正在创建shp文件...";
            IFeatureLayer ContourFeatureLayer = CreateSHP_Point(ContourPointList, fileFullPath);
            toolStripStatusLabel1.Text = "读取完毕，共" + ContourPointList.Count().ToString() + "个高程点";
            axMapControl1.AddLayer(ContourFeatureLayer);
            axMapControl1.Refresh();
        }

      
     

       
       
       
    }
}
