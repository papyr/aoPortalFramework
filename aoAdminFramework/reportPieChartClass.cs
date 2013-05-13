﻿
using System;
using System.Collections.Generic;
using System.Text;
using Contensive.BaseClasses;

namespace adminFramework
{
    public class reportPieChartClass
    {
        const int columnSize = 2;
        const int rowSize = 9999;
        const string cr = "\r\n\t";
        const string cr2 = cr + "\t";
        const string cr3 = cr2 + "\t";
        //
        struct columnStruct
        {
            public string caption;
            //public string captionClass;
            public double value;
        }
        //columnStruct[] columns = new columnStruct[columnSize];
        //bool formIncludeHeader = false;
        //
        //int columnMax = -1;
        //int columnPtr = -1;
        //
        int rowCnt = -1;
        //
        string localTitle = "";
        string localChartTitle = "";
        string localWarning = "";
        string localDescription = "";
        //string localRqs = "";
        string localHiddenList = "";
        string localButtonList = "";
        string localFormId = "";
        string localFormActionQueryString = "";
        //string localPreForm = "";
        string localHtmlBeforeContent = "";
        string localHtmlAfterContent = "";
        string localHtmlLeftOfContent = "";
        bool localIncludeForm = false;
        bool localIsEmptyReport = true;
        //int localPageSize = 20;
        //int localPageNumber = 1;
        //
        columnStruct[] row = new columnStruct[rowSize];
        //
        bool localIsOuterContainer = false;
        //
        //bool gridIncludeHeaderRow = false;
        bool gridIncludesCaptionColumn = false;
        string localgridCaptionHeader = "Slice";
        string localgridValueHeader = "Size";
        int localChartWidth = 600;
        int localChartHeight = 500;
        string localgridCaptionClass = "";
        string localgridValueClass = "";
        string localFilterListItems = "";
        //
        //-------------------------------------------------
        //
        //-------------------------------------------------
        //
        public bool isOuterContainer
        {
            get
            {
                return localIsOuterContainer;
            }
            set
            {
                localIsOuterContainer = value;
            }
        }
        //
        //-------------------------------------------------
        //
        //-------------------------------------------------
        //
        public string styleSheet
        {
            get
            {
                return Properties.Resources.styles;
                //return adminFramework.Resource1.styles.ToString();
            }
        }
        //
        //-------------------------------------------------
        //
        //-------------------------------------------------
        //
        public string javascript
        {
            get
            {
                return Properties.Resources.javascript;
            }
        }
        //
        //-------------------------------------------------
        // getResult
        //-------------------------------------------------
        //
        public string getHtml(CPBaseClass cp)
        {
            string returnHtml = "";
            string returnHeadJs = "";
            string gridRowList = "";
            int rowPtr = 0;
            string userErrors;
            string jsonData = "";
            string chartHtmlId = "afwChart" + (new Random()).Next(10000,99999);
            double total = 0;
            string grid="";
            string gridRow = "";
            string gridRowClass = "";
            string captionColumn = "";

            //
            // add user errors
            //
            userErrors = cp.Utils.EncodeText(  cp.UserError.GetList());
            if ( userErrors!="")
            {
                warning = userErrors;
            }

            //--- imported from bar chart
            //
            // headers
            //
            gridRowList += cr + "<th>" + localgridCaptionHeader + "</th>";
            gridRowList += cr + "<th>" + localgridValueHeader + "</th>";
            gridRowList = ""
                + cr + "<thead>"
                + cr2 + "<tr>"
                + indent(indent(gridRowList))
                + cr2 + "</tr>"
                + cr + "</thead>"
                + "";
            //---
            //
            // body
            //
            if (localIsEmptyReport)
            {
            }
            else
            {
                for (rowPtr = 0; rowPtr <= rowCnt; rowPtr++)
                {
                    //
                    //--- imported from bar chart
                    //
                    gridRow = "";
                    //
                    // first column is a text caption
                    //
                    if (gridIncludesCaptionColumn)
                    {
                        captionColumn = row[rowPtr].caption;
                        if (captionColumn == "")
                        {
                            captionColumn = "&nbsp;";
                        }
                        gridRow += cr + "<th class=\"" + localgridCaptionClass + "\">" + captionColumn + "</th>";
                    }
                    //
                    // additional columns are numeric
                    //
                    gridRow += cr + "<td class=\"" + localgridValueClass + "\">" + row[rowPtr].value.ToString()  + "</td>";
                    if (rowPtr % 2 == 0)
                    {
                        gridRowClass = "";
                    }
                    else
                    {
                        gridRowClass = " class=\"afwOdd\"";
                    }
                    gridRowList += ""
                        + cr + "<tr" + gridRowClass + ">"
                        + indent(gridRow)
                        + cr + "</tr>";
                    ////
                    ////--- the pie chart code
                    ////
                    //gridRowList += ""
                    //    + cr + "<div class=\"afwPieDataRow\">"
                    //    + cr2 + "<div class=\"afwPieDataValue\">" + row[rowPtr].value + "</div>"
                    //    + cr2 + "<div class=\"afwPieDataCaption\">" + row[rowPtr].caption + "</div>"
                    //    + cr + "</div>"
                    //    + "";
                    total += row[rowPtr].value;
                    jsonData += ",['" + row[rowPtr].caption + "', " + row[rowPtr].value + "]";
                }
            }
            if (jsonData != "")
            {
                jsonData = jsonData.Substring(1);
                gridRowList += ""
                    + cr + "<tfoot>"
                    + cr2+ "<tr>"
                    + cr3 + "<th class=\"" + localgridCaptionClass + "\">Total</th>"
                    + cr3 + "<td class=\"" + gridValueClass + "\">" + total + "</td>"
                    + cr2 + "</tr>"
                    + cr + "</tfoot>"
                    + "";
            }
            grid += ""
                + cr + "<tbody>"
                + indent(gridRowList)
                + cr + "</tbody>"
                + "";
            grid = ""
                + cr + "<table class=\"afwListReportTableCollapse\">"
                + indent(grid)
                + cr + "</table>";
            grid = ""
                + cr + "<div class=\"afwGridCon\">"
                + indent(grid)
                + cr + "</div>";
            returnHtml = ""
                + cr + "<div class=\"afwChartCon\" style=\"width:" + localChartWidth + "px;\">"
                + cr2 + "<div id=\"" + chartHtmlId + "\"></div>"
                + cr + "</div>"
                +cr + "<div class=\"afwGridCon\">" 
                + indent(grid) 
                + cr + "</div>";
            //returnHtml += ""
            //    + cr + "<div id=\"" + chartHtmlId + "\" class=\"afwPieChart\"></div>"
            //    + cr + "<div class=\"afwPieData\">"
            //    + indent(grid)
            //    + cr + "</div>"
            //    + "";
            returnHtml = ""
                + cr + "<div class=\"afwPieCon\">"
                + indent(returnHtml)
                + cr + "</div>"
                + "";
            if (localFilterListItems != "")
            {
                localHtmlLeftOfContent += ""
                    + cp.Html.ul( cr + "<h3>Filter Options</h3>" + cr + localFilterListItems,"","afwFilterList")
                    + "";
            }
            if (localHtmlLeftOfContent != "")
            {
                returnHtml = ""
                    + cr + "<div class=\"afwLeftSideHtml\">"
                    + indent(localHtmlLeftOfContent)
                    + cr + "</div>"
                    + cr + "<div class=\"afwRightSideHtml\">"
                    + indent(returnHtml)
                    + cr + "</div>"
                    + cr + "<div style=\"clear:both\"></div>"
                    + "";
            }
            if (localHtmlBeforeContent != "")
            {
                returnHtml = ""
                    + localHtmlBeforeContent
                    + returnHtml
                    + "";
            }
            if (localHtmlAfterContent != "")
            {
                returnHtml = ""
                    + returnHtml
                    + localHtmlAfterContent
                    + "";
            }
            //
            // headers
            //
            if (localDescription != "")
            {
                returnHtml = cr + "<p id=\"afwDescription\">" + localDescription + "</p>" + returnHtml;
            }
            if (localWarning != "")
            {
                returnHtml = cr + "<div id=\"afwWarning\">" + localWarning + "</div>" + returnHtml;
            }
            if (localTitle != "")
            {
                returnHtml = cr + "<h2 id=\"afwTitle\">" + localTitle + "</h2>" + returnHtml;
            }
            //
            // add form
            //
            if (localIncludeForm)
            {
                if (localButtonList != "")
                {
                    localButtonList = ""
                        + cr + "<div class=\"afwButtonCon\">"
                        + indent(localButtonList)
                        + cr + "</div>";
                }
                returnHtml = cr + cp.Html.Form(localButtonList + returnHtml + localButtonList + localHiddenList, "", "", "", localFormActionQueryString, "");
                //body = ""
                //    + cr + cp.Html.Form( localButtonList + body + localHiddenList )
                //    + cr + "<form action=\"" + localFormAction + "\" method=\"post\" enctype=\"MULTIPART/FORM-DATA\">"
                //    + indent(localButtonList + body + localHiddenList)
                //    + cr + "</form>";
            }
            returnHtml = cp.Html.div(returnHtml, "", "afwBodyPad", "");
            returnHtml = cp.Html.div(returnHtml, "", "afwBodyColor", "");
            returnHeadJs += ""
                + cr + "function drawChart() {"
                + cr2 + "var data = new google.visualization.DataTable();"
                + cr2 + "data.addColumn('string', 'Caption');"
                + cr2 + "data.addColumn('number', 'Values');"
                + cr2 + "data.addRows([" + jsonData + "]);"
                + cr2 + "var options = {'title':'" + localChartTitle + "','width':" + localChartWidth + ",'height':" + localChartHeight + "};"
                + cr2 + "var chart = new google.visualization.PieChart(document.getElementById('" + chartHtmlId + "'));"
                + cr2 + "chart.draw(data, options);"
                + cr + "}"
                + cr + "google.load('visualization', '1.0', {'packages':['corechart']});"
                + cr + "jQuery(document).ready(drawChart);"
                + cr + "//google.setOnLoadCallback(drawChart);"
                + cr + "";
            returnHtml += "<script Language=\"JavaScript\" type=\"text/javascript\">" + returnHeadJs + "</script>";
            returnHeadJs = "";
            //
            // if outer container, add styles and javascript
            //
            if (localIsOuterContainer)
            {
                cp.Doc.AddHeadStyle(Properties.Resources.styles);
                returnHtml = ""
                    + cr + "<div id=\"afw\">"
                    + indent(returnHtml)
                    + cr + "</div>";
            }
            //cp.Doc.AddHeadTag("<script type=\"text/javascript\" src=\"https://www.google.com/jsapi\"></script>");
            //cp.Doc.AddHeadJavascript("alert('hello world')");
            cp.Doc.AddHeadJavascript(returnHeadJs);
            return returnHtml;
        }
        //
        //-------------------------------------------------
        // add html blocks
        //-------------------------------------------------
        //
        public string htmlLeftOfTable
        {
            get
            {
                return localHtmlLeftOfContent;
            }
            set
            {
                localHtmlLeftOfContent = value;
            }
        }
        //
        public string htmlBeforeTable
        {
            get
            {
                return localHtmlBeforeContent;
            }
            set
            {
                localHtmlBeforeContent = value;
            }
        }
        //
        public string htmlAfterTable
        {
            get
            {
                return localHtmlAfterContent;
            }
            set
            {
                localHtmlAfterContent = value;
            }
        }
        //
        //-------------------------------------------------
        // add a form hidden
        //-------------------------------------------------
        //
        public void addFormHidden(string Name, string Value)
        {
            localHiddenList += cr + "<input type=\"hidden\" name=\"" + Name + "\" value=\"" + Value + "\">";
            localIncludeForm = true;
        }
        //
        //-------------------------------------------------
        // add a form button
        //-------------------------------------------------
        //
        public void addFormButton(string buttonValue)
        {
            addFormButton(buttonValue, "button", "", "");
        }
        public void addFormButton(string buttonValue, string buttonName)
        {
            addFormButton(buttonValue, buttonName, "", "");
        }
        public void addFormButton(string buttonValue, string buttonName, string buttonId)
        {
            addFormButton(buttonValue, buttonName, buttonId, "");
        }
        public void addFormButton(string buttonValue, string buttonName, string buttonId, string buttonClass)
        {
            localButtonList += cr + "<input type=\"submit\" name=\"" + buttonName + "\" value=\"" + buttonValue + "\" id=\"" + buttonId + "\" class=\"afwButton " + buttonClass + "\">";
            localIncludeForm = true;
        }
        //
        //-------------------------------------------------
        // setForm
        //-------------------------------------------------
        //
        public string formActionQueryString
        {
            get
            {
                return localFormActionQueryString;
            }
            set
            {
                localFormActionQueryString = value;
                localIncludeForm = true;
            }
        }
        public string formId
        {
            get
            {
                return localFormId;
            }
            set
            {
                localFormId = value;
                localIncludeForm = true;
            }
        }
        //
        //-------------------------------------------------
        // Refresh Query String
        //-------------------------------------------------
        //
        //public string refreshQueryString
        //{
        //    get
        //    {
        //        return localRqs;
        //    }
        //    set
        //    {
        //        localRqs = value;
        //    }
        //}
        //
        //-------------------------------------------------
        // Title
        //-------------------------------------------------
        //
        public string title
        {
            get
            {
                return localTitle;
            }
            set
            {
                localTitle = value;
            }
        }
        //
        //-------------------------------------------------
        // Title
        //-------------------------------------------------
        //
        public string chartTitle
        {
            get
            {
                return localChartTitle;
            }
            set
            {
                localChartTitle = value;
            }
        }
        //
        //-------------------------------------------------
        // Warning
        //-------------------------------------------------
        //
        public string warning
        {
            get
            {
                return localWarning;
            }
            set
            {
                localWarning = value;
            }
        }
        //
        //-------------------------------------------------
        // Description
        //-------------------------------------------------
        //
        public string description
        {
            get
            {
                return localDescription;
            }
            set
            {
                localDescription = value;
            }
        }
        ////
        ////-------------------------------------------------
        //// column properties
        ////-------------------------------------------------
        ////
        //public string columnCaption
        //{
        //    get
        //    {
        //        checkColumnPtr();
        //        return columns[columnPtr].caption;
        //    }
        //    set
        //    {
        //        if (value != "")
        //        {
        //            checkColumnPtr();
        //            formIncludeHeader = true;
        //            columns[columnPtr].caption = value;
        //        }
        //    }
        //}
        ////
        ////-------------------------------------------------
        //// set the caption class for this column
        ////-------------------------------------------------
        ////
        //public string columnCaptionClass
        //{
        //    get
        //    {
        //        checkColumnPtr();
        //        return columns[columnPtr].captionClass;
        //    }
        //    set
        //    {
        //        if (value != "")
        //        {
        //            formIncludeHeader = true;
        //            checkColumnPtr();
        //            columns[columnPtr].captionClass = value;
        //        }
        //    }
        //}
        ////
        ////-------------------------------------------------
        //// set the cell class for this column
        ////-------------------------------------------------
        ////
        //public string columnCellClass
        //{
        //    get
        //    {
        //        checkColumnPtr();
        //        return columns[columnPtr].cellClass;
        //    }
        //    set
        //    {
        //        checkColumnPtr();
        //        columns[columnPtr].cellClass = value;
        //    }
        //}
        ////
        ////-------------------------------------------------
        //// add a column
        ////-------------------------------------------------
        ////
        //public void addColumn()
        //{
        //    if (columnPtr < columnSize)
        //    {
        //        columnPtr += 1;
        //        columns[columnPtr].caption = "";
        //        columns[columnPtr].captionClass = "";
        //        columns[columnPtr].cellClass = "";
        //        if (columnPtr > columnMax)
        //        {
        //            columnMax = columnPtr;
        //        }
        //    }
        //}
        //
        //-------------------------------------------------
        //
        //-------------------------------------------------
        //
        private string indent(string src)
        {
            return src.Replace(cr, cr2);
        }
        ////
        ////-------------------------------------------------
        ////
        ////-------------------------------------------------
        ////
        //private void checkColumnPtr()
        //{
        //    if (columnPtr < 0)
        //    {
        //        addColumn();
        //    }
        //}
        //
        //-------------------------------------------------
        //
        //-------------------------------------------------
        //
        private void checkRowCnt()
        {
            if (rowCnt < 0)
            {
                addRow();
            }
        }
        //
        //-------------------------------------------------
        // chart width
        //-------------------------------------------------
        //
        public int chartWidth
        {
            get
            {
                return localChartWidth;
            }
            set
            {
                localChartWidth = value;
            }
        }
        //
        //-------------------------------------------------
        // chart height
        //-------------------------------------------------
        //
        public int chartHeight
        {
            get
            {
                return localChartHeight;
            }
            set
            {
                localChartHeight = value;
            }
        }
        //
        //-------------------------------------------------
        //
        //-------------------------------------------------
        //
        public string gridCaptionHeader
        {
            get
            {
                return localgridCaptionHeader;
            }
            set
            {
                localgridCaptionHeader = value;
                //gridIncludeHeaderRow = true;
            }
        }
        //
        //-------------------------------------------------
        //
        //-------------------------------------------------
        //
        public string gridValueHeader
        {
            get
            {
                return localgridValueHeader;
            }
            set
            {
                localgridValueHeader = value;
                //gridIncludeHeaderRow = true;
            }
        }
        //
        //-------------------------------------------------
        // row Caption for grid
        //-------------------------------------------------
        //
        public string rowCaption
        {
            get
            {
                checkRowCnt();
                return row[rowCnt].caption;
            }
            set
            {
                if (value != "")
                {
                    checkRowCnt();
                    row[rowCnt].caption = value;
                    gridIncludesCaptionColumn = true;
                    localIsEmptyReport = false;
                }
            }
        }
        //
        //-------------------------------------------------
        // row Caption Class for grid
        //-------------------------------------------------
        //
        public string gridCaptionClass
        {
            get
            {
                return localgridCaptionClass;
            }
            set
            {
                localgridCaptionClass = value;
            }
        }
        //
        //-------------------------------------------------
        // row Value
        //-------------------------------------------------
        //
        public double rowValue
        {
            get
            {
                checkRowCnt();
                return row[rowCnt].value;
            }
            set
            {
                checkRowCnt();
                row[rowCnt].value = value;
                gridIncludesCaptionColumn = true;
                localIsEmptyReport = false;
            }
        }
        //
        //-------------------------------------------------
        // row Caption Class for grid
        //-------------------------------------------------
        //
        public string gridValueClass
        {
            get
            {
                return localgridValueClass;
            }
            set
            {
                if (value != "")
                {
                    localgridValueClass = value;
                }
            }
        }
        //
        //-------------------------------------------------
        // add a row
        //-------------------------------------------------
        //
        public void addRow()
        {
            localIsEmptyReport = false;
            if (rowCnt < rowSize)
            {
                rowCnt += 1;
            }
            //columnPtr = 0;
        }
        //
        //-------------------------------------------------
        // add a filter caption
        //-------------------------------------------------
        //
        public void addFilterCaption( string caption )
        {
            localFilterListItems+= cr + "<li class=\"afwFilterCaption\">" + caption + "</li>";
        }
        //
        //-------------------------------------------------
        // add a filter input
        //-------------------------------------------------
        //
        public void addFilterInput(string formInput)
        {
            localFilterListItems += cr + "<li class=\"afwFilterInput\">" + formInput + "</li>";
        }
    }
}