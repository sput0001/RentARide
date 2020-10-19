<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chart.aspx.cs" Inherits="RentARide.Chart" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:Chart ID="Chart1" runat="server" DataSourceID="Ride">
            <series>
                <asp:Series ChartArea="ChartArea1" ChartType="Pie" Name="Series1" XValueMember="Status" YValueMembers="Column1">
                </asp:Series>
            </series>
            <chartareas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </chartareas>
        </asp:Chart>
        <asp:SqlDataSource ID="Ride" runat="server" ConnectionString="Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-RentARide-20191028050436.mdf;Initial Catalog=aspnet-RentARide-20191028050436;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [Status], count([CarId]) FROM [Cars] group by [Status];"></asp:SqlDataSource>
    </form>
</body>
</html>
