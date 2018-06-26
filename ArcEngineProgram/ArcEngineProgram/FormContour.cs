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
            toolStripStatusLabel2.Text = "";
        }
        const string WorkSpaceName = @"../Debug/Files";
        //private void AddInfo_statusbar(object o, System.EventArgs e) //更新状态栏信息
        //{
        //    toolStripStatusLabel1.Text = o.ToString();
        //}

        private List<Point3D> Read_ContourLineFile(string FileName)
        {
            FileStream fs = new FileStream(FileName, FileMode.Open);
            StreamReader sreader = new StreamReader(fs);
            List<Point3D> PointList=new List<Point3D>();
            string all_file = sreader.ReadToEnd();
            string[] signal=new string[] {"\r\n"};
            string[] split_file=all_file.Split(signal,StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split_file.Length; i++)
            {
                string[] split_line = split_file[i].Split(',');
                Point3D pt3D;
                pt3D.ID = split_line[0];
                pt3D.X = Convert.ToDouble(split_line[1]);
                pt3D.Y = Convert.ToDouble(split_line[2]);
                pt3D.Z = Convert.ToDouble(split_line[3]);
                PointList.Add(pt3D);
            }
            return PointList;
        }

        public IFields CreateShapeFields(esriGeometryType p_esriGeotype)
        {
            //创建字段编辑所需要的接口
            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit = pFields as IFieldsEdit;
            //创建基本必要的属性字段
            
            

            IField pFld = new FieldClass();
            IFieldEdit pFldEdit = pFld as IFieldEdit;
            pFldEdit.Name_2 = "shape";
            pFldEdit.IsNullable_2 = false;
            pFldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            
            //增加特定的属性字段
            pFieldsEdit.AddField(pFld);
            IGeometryDef pGeoDef = new GeometryDefClass();
            IGeometryDefEdit pGeoDefEdit = pGeoDef as IGeometryDefEdit;
            pGeoDefEdit.GeometryType_2 = p_esriGeotype;
            pGeoDefEdit.SpatialReference_2 = (ISpatialReference)new UnknownCoordinateSystem();
            pFldEdit.GeometryDef_2 = pGeoDef;
            return pFieldsEdit as IFields;
        }  

        private IFeatureLayer CreateSHP_Point(List<Point3D> PointList, string FileFullPath)
        {
            int index = FileFullPath.LastIndexOf("\\");
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileFullPath);
            string shpFolder = System.IO.Path.GetDirectoryName(FileFullPath);
            IWorkspaceFactory shpWSF = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace shpFWS = (IFeatureWorkspace)shpWSF.OpenFromFile(WorkSpaceName, 0);
            //建立基本属性表
            IFields pFields = CreateShapeFields(esriGeometryType.esriGeometryPoint);

            IFeatureClass pFeatureClass;
            string filename_shp=WorkSpaceName+@"/"+filename+".shp";
            if (System.IO.File.Exists(filename_shp))
            {
                System.IO.File.Delete(filename_shp);
                System.IO.File.Delete(System.IO.Path.ChangeExtension(filename_shp,".dbf"));
                System.IO.File.Delete(System.IO.Path.ChangeExtension(filename_shp,".shx"))
                    ;
            }
            pFeatureClass = shpFWS.CreateFeatureClass(filename, pFields,null, null, esriFeatureType.esriFTSimple, "Shape", "");
            
            
            for (int j = 0; j < PointList.Count; j++)
            {
                IPoint pPoint = new PointClass();
                pPoint.X = PointList[j].X;
                pPoint.Y = PointList[j].Y;
                IFeature pFeature = pFeatureClass.CreateFeature();
                pFeature.Shape = pPoint as IGeometry;
                pFeature.Store();
            }
            
            //完善属性表
            Complete_PropertyTable(ref pFeatureClass, PointList);

            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            pFeatureLayer.Name = filename;
            pFeatureLayer.FeatureClass = pFeatureClass;
            return pFeatureLayer;
        }

        //private IFeatureClass Create_PointFeature(List<Point3D> PointList,string FileFullPath)
        //{
        //    string FileName = System.IO.Path.GetFileName(FileFullPath);
        //    IFeatureClass MptFeatureClass;
            

        //}


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

        private IFeature Convert_Point2MultiPoint_Class(IFeatureClass PointFeatureClass)
        {
            IWorkspaceFactory contourWSF = new ShapefileWorkspaceFactoryClass();
            IFeatureWorkspace contourFWS = (IFeatureWorkspace)contourWSF.OpenFromFile(WorkSpaceName, 0);
            IFields pFields = CreateShapeFields(esriGeometryType.esriGeometryMultipoint);
            string filename = PointFeatureClass.AliasName + "_muilti";
            string filename_shp = WorkSpaceName + @"/" + filename + ".shp";
            if (System.IO.File.Exists(filename_shp))
            {
                System.IO.File.Delete(filename_shp);
                System.IO.File.Delete(System.IO.Path.ChangeExtension(filename_shp, ".dbf"));
                System.IO.File.Delete(System.IO.Path.ChangeExtension(filename_shp, ".shx"));
            }
            contourFWS.CreateFeatureClass(filename, pFields, null, null, esriFeatureType.esriFTSimple, "Shape", null);
            IFeatureClass MultiFeatureClass=contourFWS.OpenFeatureClass(filename);
            IPointCollection pPointCollection = new MultipointClass();
            for (int i = 0; i < PointFeatureClass.FeatureCount(null); i++)
            {
                IFeature pFeature = PointFeatureClass.GetFeature(i);
                IPoint pPoint = pFeature.Shape as IPoint;
                pPointCollection.AddPoint(pPoint);
            }
            IFeature MultiFeature = MultiFeatureClass.CreateFeature();
            MultiFeature.Shape = pPointCollection as IGeometry;
            MultiFeature.Store();
            return MultiFeature;
        }

        private IPointCollection Convert_Point2MultiPoint(IFeatureClass PointFeatureClass)
        {
            IPointCollection pPointCollection = new MultipointClass();
            for (int i = 0; i < PointFeatureClass.FeatureCount(null); i++)
            {
                IFeature pFeature = PointFeatureClass.GetFeature(i);
                IPoint pPoint = pFeature.Shape as IPoint;
                pPointCollection.AddPoint(pPoint);
            }
            return pPointCollection;
        }

        private void 原始测量文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "pnt文件(*.pnt)|*.pnt";
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            string fileFullPath = openFileDialog1.FileName;
            List<Point3D> ContourPointList = new List<Point3D>();
            this.toolStripStatusLabel1.Text="正在读取pnt文件...";
            ContourPointList = Read_ContourLineFile(fileFullPath);    
            this.toolStripStatusLabel1.Text = "正在创建shp文件...";
            IFeatureLayer ContourFeatureLayer = CreateSHP_Point(ContourPointList, fileFullPath);                     
            this.toolStripStatusLabel1.Text = "读取完毕，共" + ContourPointList.Count().ToString() + "个高程点";
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
            pTinLayer.Name = player.Name;
            pTinLayer.Dataset = shp2TIN;
            axSceneControl1.Scene.AddLayer(pTinLayer as ILayer, true);
            axSceneControl1.Refresh();
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
            string contourFileName=pTinLayer.Name+"_Contour";
            string contourFileName_shp = WorkSpaceName + @"/" + contourFileName + ".shp";
            if (System.IO.File.Exists(contourFileName_shp))
            {
                System.IO.File.Delete(contourFileName_shp);
                System.IO.File.Delete(System.IO.Path.ChangeExtension(contourFileName_shp,".dbf"));
                System.IO.File.Delete(System.IO.Path.ChangeExtension(contourFileName_shp,".shx"));
            }
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
            IFeatureClass PointFeatureClass = null;
            for (int i = 0; i < axMapControl1.LayerCount; i++)
            {
                ILayer pFeatureLayer = axMapControl1.get_Layer(i);
                IFeatureLayer pFlayer = pFeatureLayer as IFeatureLayer;
                IFeatureClass pFClass = pFlayer.FeatureClass;
                if (pFClass.ShapeType == esriGeometryType.esriGeometryPoint)
                {
                    PointFeatureClass = pFClass;
                    break;
                }
            }
            IGeometry ConvexHullGeometry = null;
            //IFeature MultiPointFeature = Convert_Point2MultiPoint__Class(PointFeatureClass); ;
            IPointCollection ptCollection = Convert_Point2MultiPoint(PointFeatureClass);
            //ITopologicalOperator topOperator = MultiPointFeature.Shape as ITopologicalOperator;
            ITopologicalOperator topOperator = ptCollection as ITopologicalOperator;
            ConvexHullGeometry = topOperator.ConvexHull();
            IPolygon CHpolygon = ConvexHullGeometry as IPolygon;
            IArea pArea = ConvexHullGeometry as IArea;
            MessageBox.Show(pArea.Area.ToString());
        }

        private void FormContour_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        //private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        //{
            
        //}

       
       

       
       


        
   }
}
