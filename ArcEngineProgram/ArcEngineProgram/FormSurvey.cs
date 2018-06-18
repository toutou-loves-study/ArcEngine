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
        IPoint firstPoint;

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
            flagSelectFeature = 选择测区范围ToolStripMenuItem.Checked;
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (flagSelectFeature == true)
            {
                axMapControl1.Map.ClearSelection();
                IGeometry geometry = axMapControl1.TrackRectangle();
                firstPoint = new ESRI.ArcGIS.Geometry.Point();
                firstPoint.X = e.mapX;
                firstPoint.Y = e.mapY;
                //创建sptialFilter
                ISpatialFilter spatialFilter = new SpatialFilterClass();
                spatialFilter.Geometry = geometry;
                spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

                ILayer pLayer = axMapControl1.get_Layer(0);
                IFeatureLayer pFLayer = pLayer as IFeatureLayer;
                IFeatureClass pFClass = pFLayer.FeatureClass;
                IFeatureCursor featureCursor = pFClass.Search(spatialFilter, true);
                IFeature pFeature = featureCursor.NextFeature();
                while (pFeature != null)
                {
                    axMapControl1.Map.SelectFeature(pLayer, pFeature);
                    pFeature = featureCursor.NextFeature();
                }
                axMapControl1.Refresh();
            }
            else if (flagCreateFeature == true)
            {
                ILayer pLayer = axMapControl1.get_Layer(0);
                IFeatureLayer pFLayer = pLayer as IFeatureLayer;
                IFeatureClass pFClass = pFLayer.FeatureClass;
                IGeometry geometry = null;
                switch (pFClass.ShapeType)
                {
                    case esriGeometryType.esriGeometryPoint:
                        IPoint point = new PointClass();
                        point.PutCoords(e.mapX, e.mapY);
                        geometry = point as IGeometry;
                        break;
                    case esriGeometryType.esriGeometryPolygon:
                        geometry = axMapControl1.TrackPolygon();
                        break;
                }
                IFeature pFeature = pFClass.CreateFeature();
                pFeature.Shape = geometry;
                pFeature.Store();

                axMapControl1.Refresh();
                flagCreateFeature = false;
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
            IFeatureLayer layer;
            layer = (IFeatureLayer)axMapControl1.get_Layer(0);
            IGeoFeatureLayer geoFeatureLayer = layer as IGeoFeatureLayer;
            SimpleRenderer simpleRender = new SimpleRendererClass();
            ISimpleMarkerSymbol pMarkerSymbol;
            IColor color;
            color = new RgbColorClass();
            color.RGB = 200;
            pMarkerSymbol = new SimpleMarkerSymbolClass();
            pMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSX;
            pMarkerSymbol.Color = color;
            pMarkerSymbol.Angle = 60;
            pMarkerSymbol.Size = 10;
            simpleRender.Symbol = pMarkerSymbol as ISymbol;
            geoFeatureLayer.Renderer = simpleRender as IFeatureRenderer;
            axMapControl1.Refresh();
        }

        private void 删除控制点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ISelection pSeletion = axMapControl1.Map.FeatureSelection;
            IEnumFeature pEnumFeature = (IEnumFeature)pSeletion;

            IFeature pFeature = pEnumFeature.Next();
            while (pFeature != null)
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
    }
}
