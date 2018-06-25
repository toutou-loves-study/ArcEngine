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
using ESRI.ArcGIS;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

namespace ArcEngineProgram
{
    public partial class FormSurvey : Form
    {
        bool flagSelectFeature = false;
        bool flagCreateFeature = false;
        bool flagSelectStation = false;
        IElement rectpElement;

        public IColor Color2IColor(Color color)
        {

            IColor pColor = new RgbColorClass();

            pColor.RGB = color.B * 65536 + color.G * 256 + color.R;

            return pColor;

        }
        ILayer pMovelayer;

        public FormSurvey()
        {
            InitializeComponent();
        }
        public DataTable GetLayerData(IFeatureLayer layer)
        {
            IFeature pFeature = null;
            DataTable pFeatDT = new DataTable();
            DataRow pDataRow = null;
            DataColumn pDataCol = null;
            IField pField = null;
            for (int i = 0; i < layer.FeatureClass.Fields.FieldCount; i++)
            {
                pDataCol = new DataColumn();
                pField = layer.FeatureClass.Fields.get_Field(i);
                pDataCol.ColumnName = pField.AliasName; //获取字段名作为列标题
                pDataCol.DataType = Type.GetType("System.Object");//定义列字段类型
                pFeatDT.Columns.Add(pDataCol); //在数据表中添加字段信息
            }
            IFeatureCursor pFeatureCursor = layer.Search(null, true);
            pFeature = pFeatureCursor.NextFeature();
            while (pFeature != null)
            {
                pDataRow = pFeatDT.NewRow();
                //获取字段属性
                for (int k = 0; k < pFeatDT.Columns.Count; k++)
                {
                    pDataRow[k] = pFeature.get_Value(k);
                }

                pFeatDT.Rows.Add(pDataRow); //在数据表中添加字段属性信息
                pFeature = pFeatureCursor.NextFeature();
            }
            //释放指针
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            //dataGridAttribute.BeginInit();
            return pFeatDT;

        }


        private void 加载Shp文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "shp文件(*.shp)|*.shp";
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            string fileFullPath = openFileDialog1.FileName;
            string pFolder = System.IO.Path.GetDirectoryName(fileFullPath);
            string pFileName = System.IO.Path.GetFileName(fileFullPath);
            axMapControl1.AddShapeFile(pFolder, pFileName);
            axMapControl1.Extent = axMapControl1.FullExtent;
            axMapControl1.Refresh();

            axMapControl2.AddShapeFile(pFolder, pFileName);
            axMapControl2.Extent = axMapControl1.FullExtent;
            axMapControl2.Refresh();

        }

        private void axMapControl1_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            //获取当前的主窗体的坐标范围
            IEnvelope envelope = (IEnvelope)e.newEnvelope;
            //获取鹰眼视图画布容器
            IGraphicsContainer graphicsContatiner = axMapControl2.ActiveView.GraphicsContainer;
            graphicsContatiner.DeleteAllElements();
            //创建矩形元素
            IElement element = new RectangleElementClass();
            element.Geometry = envelope;
            //创建矩形轮廓的样式
            ILineSymbol linesymbol = new SimpleLineSymbolClass();
            linesymbol.Width = 2;

            IRgbColor rgbColor = new RgbColorClass();
            rgbColor.Red = 200;
            rgbColor.Green = 100;
            rgbColor.Blue = 0;
            rgbColor.Transparency = 255;
            linesymbol.Color = rgbColor;

            IFillSymbol fillsymbol = new SimpleFillSymbolClass();
            rgbColor.Transparency = 0;
            fillsymbol.Color = rgbColor;
            fillsymbol.Outline = linesymbol;

            IFillShapeElement fillshapeElement = element as IFillShapeElement;
            fillshapeElement.Symbol = fillsymbol;
            // 添加绘图元素

            graphicsContatiner.AddElement((IElement)fillshapeElement, 0);
            axMapControl2.Refresh();
        }

        private void 删除图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            esriTOCControlItem pTocItem = new esriTOCControlItem();

            ILayer pLayer = new FeatureLayerClass();
            IBasicMap pBasicMap = new MapClass();
            object obj1 = new object();
            object obj2 = new object();
            axTOCControl1.GetSelectedItem(ref pTocItem, ref pBasicMap, ref pLayer, ref obj1, ref obj2);
            if (pTocItem == esriTOCControlItem.esriTOCControlItemLayer)
            {
                int iIndex;
                for (iIndex = 0; iIndex < axMapControl1.LayerCount; iIndex++)
                {
                    if (axMapControl1.get_Layer(iIndex) == pLayer)
                    {
                        axMapControl1.DeleteLayer(iIndex);
                        break;
                    }
                }
            }
        }

        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            this.axLicenseControl1.ContextMenuStrip = null;
            IBasicMap map = new MapClass();
            object other = null;
            object index = null;
            ILayer layer = new FeatureLayerClass();
            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            try
            {
                this.axTOCControl1.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            switch (e.button)
            {
                case 1:
                    if (item == esriTOCControlItem.esriTOCControlItemLayer)
                    {
                        if (layer is IAnnotationSublayer)
                            return;
                        else
                            pMovelayer = layer;
                        dataGridView1.DataSource = GetLayerData(layer as IFeatureLayer);
                    }
                    break;
                case 2://右键
                    System.Drawing.Point pt = new System.Drawing.Point(MousePosition.X, MousePosition.Y);
                    contextMenuStrip1.Show(pt);
                    break;
            }
        }

        private void axTOCControl1_OnMouseUp(object sender, ITOCControlEvents_OnMouseUpEvent e)
        {

            if (e.button == 1 && pMovelayer != null)
            {
                esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap map = null;
                ILayer layer = null;
                object other = null;
                object index = null;
                axTOCControl1.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);
                IMap pMap = axMapControl1.ActiveView.FocusMap;
                ILayer pTempLayer;
                int toindex = 0;
                if (item == esriTOCControlItem.esriTOCControlItemLayer && layer != null)
                {
                    if (pMovelayer != layer)
                    {
                        for (int i = 0; i < pMap.LayerCount; i++)
                        {
                            pTempLayer = pMap.get_Layer(i);
                            if (pTempLayer == layer)
                            {
                                toindex = i; ;
                            }
                        }
                        pMap.MoveLayer(pMovelayer, toindex);
                    }
                }

                axMapControl1.ActiveView.Refresh();
                axMapControl1.Update();
                pMovelayer = null;

            }
        }


        private void 选择测区范围ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            flagSelectFeature = 选择测区范围ToolStripMenuItem.Checked;
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (flagSelectFeature == true) //选择实体
            {

                axMapControl1.Map.ClearSelection();
                IGeometry geometry = axMapControl1.TrackRectangle();
                IPoint p1, p2, p3, p4;
                p1 = new PointClass();
                p2 = new PointClass();
                p3 = new PointClass();
                p4 = new PointClass();
                p1.PutCoords(geometry.Envelope.XMin, geometry.Envelope.YMin);
                p2.PutCoords(geometry.Envelope.XMin, geometry.Envelope.YMax);
                p3.PutCoords(geometry.Envelope.XMax, geometry.Envelope.YMax);
                p4.PutCoords(geometry.Envelope.XMax, geometry.Envelope.YMin);
                IGeometryCollection pPolyline = new PolylineClass();
                object o = Type.Missing;
                
                for (int i = 0; i < 4; i++)
                {
                    IPointCollection pPath = new PathClass();
                    switch(i)
                    {
                        case 0:
                            pPath.AddPoint(p1, ref o, ref o);
                            pPath.AddPoint(p2, ref o, ref o);
                            break;
                        case 1:
                            pPath.AddPoint(p2, ref o, ref o);
                            pPath.AddPoint(p3, ref o, ref o);
                            break;
                        case 2:
                            pPath.AddPoint(p3, ref o, ref o);
                            pPath.AddPoint(p4, ref o, ref o);
                            break;
                        case 3:
                            pPath.AddPoint(p4, ref o, ref o);
                            pPath.AddPoint(p1, ref o, ref o);
                            break;
                    }
                    pPolyline.AddGeometry(pPath as IGeometry, ref o, ref o);
               }
                IGeometry polyline=pPolyline as IGeometry;
                ILineElement pLineElement=new LineElementClass();
                rectpElement=pLineElement as IElement;
                rectpElement.Geometry=polyline;
                IGraphicsContainer graphicsContainer = axMapControl1.ActiveView.GraphicsContainer;
                graphicsContainer.DeleteAllElements();

                graphicsContainer.AddElement(rectpElement, 0);
                axMapControl1.Refresh();
                
                //创建sptialFilter
                ISpatialFilter spatialFilter = new SpatialFilterClass();
                spatialFilter.Geometry = geometry;
                spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                int iIndex = 0;
                for (iIndex = 0; iIndex < axMapControl1.LayerCount; iIndex++)
                {
                    ILayer pLayer = axMapControl1.get_Layer(iIndex);
                    IFeatureLayer pFLayer = pLayer as IFeatureLayer;
                    IFeatureClass pFClass = pFLayer.FeatureClass;
                    if (pFClass.ShapeType==esriGeometryType.esriGeometryPoint)
                    {
                        IFeatureCursor featureCursor = pFClass.Search(spatialFilter, true);
                        IFeature pFeature = featureCursor.NextFeature();
                        while (pFeature != null)
                        {
                            axMapControl1.Map.SelectFeature(pLayer, pFeature);
                            pFeature = featureCursor.NextFeature();
                        }
                        axMapControl1.Refresh();
                    }
                }
                flagSelectFeature = false;
                选择测区范围ToolStripMenuItem.Checked = false;

            }
            else if (flagCreateFeature == true) //创建实体
            {
                int iIndex;
                for (iIndex = 0; iIndex < axMapControl1.LayerCount; iIndex++)
                {
                    ILayer pLayer = axMapControl1.get_Layer(iIndex);
                    IFeatureLayer pFLayer = pLayer as IFeatureLayer;
                    IFeatureClass pFClass = pFLayer.FeatureClass;
                    IGeometry geometry = null;
                    if(pFClass.ShapeType==esriGeometryType.esriGeometryPoint)
                    {
                            IPoint point = new PointClass();
                            point.PutCoords(e.mapX, e.mapY);
                            geometry = point as IGeometry;
                            IFeature pFeature = pFClass.CreateFeature();
                            pFeature.Shape = geometry;
                            pFeature.Store();

                            axMapControl1.Refresh();
                            flagCreateFeature = false;
                            删除控制点ToolStripMenuItem.Checked = false;
                    }

                }
            }
            else if (flagSelectStation == true)
            {
                axMapControl1.Map.ClearSelection();
                IGeometry geometry = axMapControl1.TrackCircle();
                ISpatialFilter spatialFilter = new SpatialFilter();
                spatialFilter.Geometry = geometry;
                spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                int iIndex;
                for (iIndex = 0; iIndex < axMapControl1.LayerCount; iIndex++)
                {
                    ILayer pLayer = axMapControl1.get_Layer(iIndex);
                    IFeatureLayer pFLayer = pLayer as IFeatureLayer;
                    IFeatureClass pFClass = pFLayer.FeatureClass;
                    if (pFClass.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        IFeatureCursor featureCursor = pFClass.Search(spatialFilter, true);
                        IFeature pFeature = featureCursor.NextFeature();
                        while (pFeature != null)
                        {
                            axMapControl1.Map.SelectFeature(pLayer, pFeature);
                            pFeature = featureCursor.NextFeature();
                        }
                        axMapControl1.Refresh();
                        flagSelectStation = false;
                        选择当前测站ToolStripMenuItem.Checked = false;
                    }
                }
            }
        }

        private void 图层上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            esriTOCControlItem pTocItem = new esriTOCControlItem();

            ILayer pLayer = new FeatureLayerClass();
            IBasicMap pBasicMap = new MapClass();
            object obj1 = new object();
            object obj2 = new object();
            axTOCControl1.GetSelectedItem(ref pTocItem, ref pBasicMap, ref pLayer, ref obj1, ref obj2);
            if (pTocItem == esriTOCControlItem.esriTOCControlItemLayer)
            {
                int iIndex;
                for (iIndex = 0; iIndex < axMapControl1.LayerCount; iIndex++)
                {
                    if (axMapControl1.get_Layer(iIndex) == pLayer)
                    {
                        if (iIndex == 0) break;
                        axMapControl1.MoveLayerTo(iIndex, iIndex - 1);
                        break;
                    }
                }
            }
        }

        private void 图层下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            esriTOCControlItem pTocItem = new esriTOCControlItem();

            ILayer pLayer = new FeatureLayerClass();
            IBasicMap pBasicMap = new MapClass();
            object obj1 = new object();
            object obj2 = new object();
            axTOCControl1.GetSelectedItem(ref pTocItem, ref pBasicMap, ref pLayer, ref obj1, ref obj2);
            if (pTocItem == esriTOCControlItem.esriTOCControlItemLayer)
            {
                int iIndex;
                for (iIndex = 0; iIndex < axMapControl1.LayerCount; iIndex++)
                {
                    if (axMapControl1.get_Layer(iIndex) == pLayer)
                    {
                        if (iIndex == axMapControl1.LayerCount - 1) break;
                        axMapControl1.MoveLayerTo(iIndex, iIndex + 1);
                        break;
                    }
                }
            }
        }

        private void 添加控制点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flagCreateFeature = 添加控制点ToolStripMenuItem.Checked;
        }

        private void 更换控制点符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //layer = (IFeatureLayer)axMapControl1.get_Layer(0);
            int iIndex;
            for (iIndex = 0; iIndex < axMapControl1.LayerCount; iIndex++)
            {

                ILayer pLayer = axMapControl1.get_Layer(iIndex);
                IFeatureLayer pFLayer = pLayer as IFeatureLayer;
                IFeatureClass pFClass = pFLayer.FeatureClass;
                if (pFClass.ShapeType == esriGeometryType.esriGeometryPoint)
                {
                    IGeoFeatureLayer geoFeatureLayer = pLayer as IGeoFeatureLayer;
                    SimpleRenderer simpleRender = new SimpleRendererClass();
                    ISimpleMarkerSymbol pMarkerSymbol;
                    Random rd = new Random();
                    Color mycolor = Color.FromArgb(0, rd.Next(0,256), rd.Next(0,256), rd.Next(0,256));
                    IColor color = Color2IColor(mycolor);
                    pMarkerSymbol = new SimpleMarkerSymbolClass();
                    pMarkerSymbol.Style = (esriSimpleMarkerStyle)rd.Next(0, 5);
                    pMarkerSymbol.Color = color;
                    pMarkerSymbol.Angle = 60;
                    pMarkerSymbol.Size = 6;
                    simpleRender.Symbol = pMarkerSymbol as ISymbol;
                    geoFeatureLayer.Renderer = simpleRender as IFeatureRenderer;
                    axMapControl1.Refresh();
                }
            }
        }

        private void 删除控制点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ISelection pSeletion = axMapControl1.Map.FeatureSelection;
            IEnumFeature pEnumFeature = (IEnumFeature)pSeletion;

            IFeature pFeature = pEnumFeature.Next();
            while (pFeature != null && pFeature.Shape.GeometryType==esriGeometryType.esriGeometryPoint)
            {
                pFeature.Delete();
                pFeature = pEnumFeature.Next();
            }
            axMapControl1.Refresh();
        }

        private void axMapControl2_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            IPoint point = new PointClass();
            point.X = e.mapX;
            point.Y = e.mapY;
            axMapControl1.CenterAt(point);
        }

        private void 坐标添加控制点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormControlPoints Acp = new FormControlPoints();
            if (Acp.ShowDialog() == DialogResult.OK)
            {
                int iIndex;
                for (iIndex = 0; iIndex < axMapControl1.LayerCount; iIndex++)
                {

                    ILayer pLayer = axMapControl1.get_Layer(iIndex);
                    IFeatureLayer pFLayer = pLayer as IFeatureLayer;
                    IFeatureClass pFClass = pFLayer.FeatureClass;
                    IGeometry geometry = null;

                    if (pFClass.ShapeType == esriGeometryType.esriGeometryPoint)
                    {
                        IPoint point = new PointClass();
                        point.PutCoords(GlobalData.x, GlobalData.y);
                        geometry = point as IGeometry;
                        IFeature pFeature = pFClass.CreateFeature();
                        pFeature.Shape = geometry;
                        pFeature.Store();

                        axMapControl1.Refresh();
                    }
                }
            }
        }

        private void 选择当前测站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            flagSelectStation = true;
        }

        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IGeometry buffer;
            ISelection pSeletion = axMapControl1.Map.FeatureSelection;
            IEnumFeature pEnumFeature = (IEnumFeature)pSeletion;
            //IGraphicsContainer graphicsContainer = axMapControl1.ActiveView.GraphicsContainer;
            //graphicsContainer.DeleteAllElements();
            IFeature pFeature = pEnumFeature.Next();//自己
            double bufferDistance = GlobalData.dist;
            if (bufferDistance <= 0.0)
            {
                MessageBox.Show("距离设置错误");
                return;
            }
            if(pFeature != null)
            {
                axMapControl1.Map.ClearSelection();
                ITopologicalOperator topoOperator = pFeature.Shape as ITopologicalOperator;
                buffer = topoOperator.Buffer(bufferDistance);
                ISpatialFilter spatilaFilter = new SpatialFilterClass(); //在缓冲区内
                spatilaFilter.Geometry = buffer;
                spatilaFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                int iIndex;
                object o=Type.Missing;
                IPoint pt0 = pFeature.Shape as IPoint;
                for (iIndex = 0; iIndex < axMapControl1.LayerCount; iIndex++)
                {
                    ILayer pLayer = axMapControl1.get_Layer(iIndex);
                    IFeatureLayer pFLayer = pLayer as IFeatureLayer;
                    IFeatureClass pFClass = pFLayer.FeatureClass;
                    ILayer lineLayer = axMapControl1.get_Layer(axMapControl1.LayerCount-1 - iIndex);
                    IFeatureLayer lineFLayer = lineLayer as IFeatureLayer;
                    IFeatureClass lineFClass = lineFLayer.FeatureClass;
                    if (pFClass.ShapeType == esriGeometryType.esriGeometryPoint) //点图层
                    {
                        //IWorkspaceFactory pWSF=new 

                        IFeatureCursor featureCursor = pFClass.Search(spatilaFilter, true);//圈内的点
                        IFeature oFeature = featureCursor.NextFeature();

                        while (oFeature != null)
                        {
                            if (oFeature.OID != pFeature.OID)
                            {
                                IFeatureCursor polygonCursor = lineFClass.Search(spatilaFilter, true);
                                IFeature polyFeature = polygonCursor.NextFeature();
                                IGeometryCollection polyline = new PolylineClass();
                                IPoint pt1 = oFeature.Shape as IPoint;
                                IPointCollection pPath = new PathClass();
                                
                                pPath.AddPoint(pt0, ref o, ref o);
                                pPath.AddPoint(pt1, ref o, ref o);
                                polyline.AddGeometry(pPath as IGeometry);
                                ITopologicalOperator pTopoOperator = polyline as ITopologicalOperator;
                                while (polyFeature != null)
                                {
                                    IPolyline pPolylineresult=pTopoOperator.Intersect(polyFeature.Shape,esriGeometryDimension.esriGeometry1Dimension) as IPolyline;
                                    if (pPolylineresult.Length!=0)
                                    {
                                        break;
                                    }
                                    polyFeature = polygonCursor.NextFeature();
                                }
                                if (polyFeature == null)//normal end
                                {
                                    axMapControl1.Map.SelectFeature(pLayer, oFeature);//找出处自己之外的伙伴
                                }
                            }
                            oFeature = featureCursor.NextFeature();
                        }
                        break;
                        
                    }

                }


                axMapControl1.Refresh();
            }


        }

        private void 输入仪器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDistance fd = new FormDistance();
            fd.ShowDialog();
    
        }

        private void 给出可测区域ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IGeometry buffer;
            ISelection pSeletion = axMapControl1.Map.FeatureSelection;
            IEnumFeature pEnumFeature = (IEnumFeature)pSeletion;
            IGraphicsContainer graphicsContainer = axMapControl1.ActiveView.GraphicsContainer;

            //graphicsContainer.DeleteAllElements();
            IFeature pFeature = pEnumFeature.Next(); //选择集
            double bufferDistance = GlobalData.dist;
            if (bufferDistance <= 0.0)
            {
                MessageBox.Show("距离设置错误");
                return;
            }


            while (pFeature != null)
            {
                ITopologicalOperator topoOperator = pFeature.Shape as ITopologicalOperator;
                buffer = topoOperator.Buffer(bufferDistance);
                topoOperator = buffer as ITopologicalOperator;
                IGeometry result;
                if(rectpElement!=null)
                    topoOperator.Clip(rectpElement.Geometry.Envelope);
                IElement element = new PolygonElementClass();
                element.Geometry = buffer;

                //创建矩形轮廓的样式
                ILineSymbol linesymbol = new SimpleLineSymbolClass();
                linesymbol.Width = 2;

                IRgbColor rgbColor = new RgbColorClass();
                rgbColor.Red = 65;
                rgbColor.Green = 105;
                rgbColor.Blue =225;
                rgbColor.Transparency = 255;
                linesymbol.Color = rgbColor;

                IFillSymbol fillsymbol = new SimpleFillSymbolClass();

                rgbColor.Transparency = 0;
                fillsymbol.Color = rgbColor;
                fillsymbol.Outline = linesymbol;

                IFillShapeElement fillshapeElement = element as IFillShapeElement;
                fillshapeElement.Symbol = fillsymbol;
                // 添加绘图元素
                graphicsContainer.AddElement(element, 0);
                pFeature = pEnumFeature.Next();
            }
            axMapControl1.Refresh();
        }

        private void 清空容器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IGraphicsContainer gc = axMapControl1.ActiveView.GraphicsContainer;
            gc.DeleteAllElements();
            axMapControl1.Map.ClearSelection();
            axMapControl1.Refresh();
        }

        private void FormSurvey_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
