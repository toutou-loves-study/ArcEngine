using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using System.IO;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Analyst3D;

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
        const string WorkSpaceName = "File";
        //private void AddInfo_statusbar(object o, System.EventArgs e) //更新状态栏信息
        //{
        //    toolStripStatusLabel1.Text = o.ToString();
        //}

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

        public IFields CreateShapeFields(esriGeometryType p_esriGeotype)
        {
            //创建字段编辑所需要的接口
            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit = pFields as IFieldsEdit;
            //给字段属性、类型赋值
            IGeometryDef pGeoDef = new GeometryDefClass();
            IGeometryDefEdit pGeoDefEdit = pGeoDef as IGeometryDefEdit;
            pGeoDefEdit.GeometryType_2 = p_esriGeotype;
            pGeoDefEdit.SpatialReference_2 = (ISpatialReference)new UnknownCoordinateSystem();

            IField pFld = new FieldClass();
            IFieldEdit pFldEdit = pFld as IFieldEdit;
            pFldEdit.Name_2 = "shape";
            pFldEdit.IsNullable_2 = false;
            pFldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            pFldEdit.GeometryDef_2 = pGeoDef;

            pFieldsEdit.AddField(pFld);

            return pFields;
        }  

        private IFeatureLayer CreateSHP_Point(List<Point3D> PointList, string FileFullPath)
        {
            int index = FileFullPath.LastIndexOf("\\");
            string filename = System.IO.Path.GetFileName(FileFullPath);
            string shpFolder = System.IO.Path.GetDirectoryName(FileFullPath);
            IWorkspaceFactory shpWSF = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace shpFWS = (IFeatureWorkspace)shpWSF.OpenFromFile(FileFullPath.Substring(0,index), 0);
            //建立基本属性表
            IFields pFields = CreateShapeFields(esriGeometryType.esriGeometryPoint);

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
            //完善属性表
            Complete_PropertyTable(ref pFeatureClass, PointList);

            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            pFeatureLayer.Name = filename;
            pFeatureLayer.FeatureClass = pFeatureClass;
            return pFeatureLayer;
        }

        private void Complete_PropertyTable(ref IFeatureClass pFeatureClass,List<Point3D> PointList)
        {
            string[] FieldName = { "ptName", "X", "Y", "Z" };
            IClass pTable = pFeatureClass as IClass;        //use ITable or IClass      
            for (int i = 0; i < FieldName.Length; i++)
            {
                IField pField = new FieldClass();
                IFieldEdit pFieldEdit;
                pFieldEdit =pField as  IFieldEdit;
                pFieldEdit.Name_2 = FieldName[i];
                if (i == 0)
                {
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                }
                else
                {
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                }
                pTable.AddField(pFieldEdit);
            }
            for (int i = 0; i < pFeatureClass.FeatureCount(null); i++)
            {
                IFeature pFeature = pFeatureClass.GetFeature(i);
                pFeature.set_Value(pFeatureClass.FindField(FieldName[0]), PointList[i].ID);
                pFeature.set_Value(pFeatureClass.FindField(FieldName[1]), PointList[i].X);
                pFeature.set_Value(pFeatureClass.FindField(FieldName[2]), PointList[i].Y);
                pFeature.set_Value(pFeatureClass.FindField(FieldName[3]), PointList[i].Z);
                pFeature.Store();
            }
        }

        private void Display_PropertyTable(IFeatureLayer pFeatureLayer)
        {
            IFeatureLayer pFLayer = pFeatureLayer as IFeatureLayer;
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            if (pFeatureClass == null) return;
            DataTable dt = new DataTable();
            DataColumn dc = null;
            for (int i = 0; i < pFeatureClass.Fields.FieldCount; i++)      //先生成各个字段对应的空白列 相当于读出表头
            {

                dc = new DataColumn(pFeatureClass.Fields.get_Field(i).Name);

                dt.Columns.Add(dc);

            }
            IFeatureCursor pFeatureCuror = pFeatureClass.Search(null, false);
            IFeature pFeature = pFeatureCuror.NextFeature();

            DataRow dr = null;
            while (pFeature != null)                                             //获得每个点的属性
            {
                dr = dt.NewRow();
                for (int j = 0; j < pFeatureClass.Fields.FieldCount; j++)
                {
                    if (pFeatureClass.FindField(pFeatureClass.ShapeFieldName) == j)
                    {

                        dr[j] = pFeatureClass.ShapeType.ToString();
                    }
                    else
                    {
                        dr[j] = pFeature.get_Value(j).ToString();

                    }
                }

                dt.Rows.Add(dr);
                pFeature = pFeatureCuror.NextFeature();
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
        }

        private ITin Create_TIN(IFeatureClass pFeatureClass, IField pField)
        {
            IGeoDataset pGeoDataset = pFeatureClass as IGeoDataset;
            ITinEdit pTinEdit = new TinClass();
            pTinEdit.InitNew(pGeoDataset.Extent);
            object pObj = Type.Missing;

            pTinEdit.AddFromFeatureClass(pFeatureClass, null, pField, null, esriTinSurfaceType.esriTinMassPoint, ref pObj);
            pTinEdit.Refresh();
            return pTinEdit as ITin;
        }

 
        private IFeatureClass Create_ContourLine(ITin pTin,string WorkSpaceName,string FileName)
        {
            int pInterval = 1;
            IWorkspaceFactory contourWSF = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace contourFWS = (IFeatureWorkspace)contourWSF.OpenFromFile(WorkSpaceName, 0);
            IFields pFields = CreateShapeFields(esriGeometryType.esriGeometryPolyline);
            contourFWS.CreateFeatureClass(FileName, pFields, null, null, esriFeatureType.esriFTSimple, "Shape", null);
            IFeatureClass pContourFeatureClass = contourFWS.OpenFeatureClass(FileName);
            //生成等高线  
            ITinSurface pTinSurface = pTin as ITinSurface;
            pTinSurface.Contour(0, pInterval, pContourFeatureClass, "Contour", 0);
            return pContourFeatureClass;
        }


        private void 原始测量文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "pnt文件(*.pnt)|*.pnt";
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            string fileFullPath = openFileDialog1.FileName;
            List<Point3D> ContourPointList = new List<Point3D>();
            toolStripStatusLabel1.Text="正在读取pnt文件...";
            ContourPointList = Read_ContourLineFile(fileFullPath);                  
            toolStripStatusLabel1.Text = "正在创建shp文件...";
            IFeatureLayer ContourFeatureLayer = CreateSHP_Point(ContourPointList, fileFullPath);                     
            toolStripStatusLabel1.Text = "读取完毕，共" + ContourPointList.Count().ToString() + "个高程点";
            axMapControl1.AddLayer(ContourFeatureLayer);
            axMapControl1.Refresh();
            Display_PropertyTable(ContourFeatureLayer);

        }

        private void 建立TINToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ILayer player = axMapControl1.get_Layer(0);
            IFeatureLayer pFlayer = player as IFeatureLayer;
            IFeatureClass pFClass = pFlayer.FeatureClass;
            IField pField=pFClass.Fields.get_Field(pFClass.FindField("Z"));
            ITin shp2TIN = Create_TIN(pFClass, pField);
            ITinLayer pTinLayer = new TinLayerClass();
            pTinLayer.Dataset = shp2TIN;
            axSceneControl1.Scene.AddLayer(pTinLayer as ILayer, true);
        }

        private IColor Get_RGBColor(int red, int green, int blue)
        {
            IRgbColor pIrgbColor = new RgbColorClass();
            pIrgbColor.Blue = blue;
            pIrgbColor.Red = red;
            pIrgbColor.Green = green;
            return pIrgbColor as IColor;
        }


        private void 生成等高线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //获取TIN图层
            ITinLayer  pTinLayer = axSceneControl1.Scene.get_Layer(0) as ITinLayer;
            ITin pTin = pTinLayer.Dataset as ITin;
            string contourFileName=pTinLayer.Name;
            IFeatureClass contourFeatureClass = Create_ContourLine(pTin, WorkSpaceName, contourFileName);
            //添加等高线图层  
            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            pFeatureLayer.FeatureClass = contourFeatureClass;
            IGeoFeatureLayer pGeoFeatureLayer = pFeatureLayer as IGeoFeatureLayer;
            pGeoFeatureLayer.DisplayAnnotation = true;
            pGeoFeatureLayer.DisplayField = "Contour";
            pGeoFeatureLayer.Name = contourFileName + "_Contour";
            //设置线样式  
            ILineSymbol pLineSymbol = new SimpleLineSymbolClass();
            pLineSymbol.Color = Get_RGBColor(100, 50, 30);
            pLineSymbol.Width = 2;
            ISimpleRenderer pRender = pGeoFeatureLayer.Renderer as ISimpleRenderer;
            pRender.Symbol = pLineSymbol as ISymbol;
            axMapControl1.AddLayer(pFeatureLayer as ILayer);
        }

        private void 计算测区范围ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        //    for (int i = 0; i < axMapControl1.LayerCount; i++)
        //    {
        //        ILayer pFeatureLayer = axMapControl1.get_Layer(i);
        //        IFeatureLayer pFlayer = pFeatureLayer as IFeatureLayer;
        //        IFeatureClass pFClass = pFlayer.FeatureClass;
                
        //    }
        //    axMapControl1.Map.ClearSelection();
        //    ITopologicalOperator topOperator = feature1.Shape as ITopologicalOperator;
        //    IGeometry geometry = topOperator.Intersect(feature1.Shape as IGeometry, esriGeometryDimension.esriGeometry2Dimension);
        //    IGraphicsContainer graphicsContainer = axMapControl1.ActiveView.GraphicsContainer;
        //    graphicsContainer.DeleteAllElements();
        //    IElement element = new PolygonElementClass();
        //    element.Geometry = geometry;

        //    graphicsContainer.AddElement(element, 1);
        //    axMapControl1.Refresh(); ;
        }

        private void FormContour_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

       
       


        
   }
}
