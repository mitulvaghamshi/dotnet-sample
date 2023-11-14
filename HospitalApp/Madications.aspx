<%@ Page Title="Madications" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Madications.aspx.cs" Inherits="HospitalApp.Madications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <asp:GridView ID="madicationsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="patient_id,admission_date" DataSourceID="MadicationsSqlDataSource" CssClass="table-borderless" AllowPaging="True">
        <Columns>
            <asp:BoundField DataField="admission_date" HeaderText="Addmission Date"></asp:BoundField>
            <asp:BoundField DataField="discharge_date" HeaderText="Discharge Date"></asp:BoundField>
            <asp:BoundField DataField="primary_diagnosis" HeaderText="Diagnosis"></asp:BoundField>
            <asp:BoundField DataField="secondary_diagnoses" HeaderText="Other Diagnosis"></asp:BoundField>
            <asp:BoundField DataField="room" HeaderText="Room No"></asp:BoundField>
            <asp:BoundField DataField="bed" HeaderText="Bed No"></asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource runat="server" ID="MadicationsSqlDataSource" ConnectionString='<%$ ConnectionStrings:CHDBConnectionString %>' DeleteCommand="DELETE FROM [admissions] WHERE [patient_id] = @patient_id AND [admission_date] = @admission_date" InsertCommand="INSERT INTO [admissions] ([patient_id], [admission_date], [discharge_date], [primary_diagnosis], [secondary_diagnoses], [attending_physician_id], [nursing_unit_id], [room], [bed]) VALUES (@patient_id, @admission_date, @discharge_date, @primary_diagnosis, @secondary_diagnoses, @attending_physician_id, @nursing_unit_id, @room, @bed)" SelectCommand="SELECT * FROM [admissions]" UpdateCommand="UPDATE [admissions] SET [discharge_date] = @discharge_date, [primary_diagnosis] = @primary_diagnosis, [secondary_diagnoses] = @secondary_diagnoses, [attending_physician_id] = @attending_physician_id, [nursing_unit_id] = @nursing_unit_id, [room] = @room, [bed] = @bed WHERE [patient_id] = @patient_id AND [admission_date] = @admission_date">
        <DeleteParameters>
            <asp:Parameter Name="patient_id" Type="Int32"></asp:Parameter>
            <asp:Parameter DbType="Date" Name="admission_date"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="patient_id" Type="Int32"></asp:Parameter>
            <asp:Parameter DbType="Date" Name="admission_date"></asp:Parameter>
            <asp:Parameter DbType="Date" Name="discharge_date"></asp:Parameter>
            <asp:Parameter Name="primary_diagnosis" Type="String"></asp:Parameter>
            <asp:Parameter Name="secondary_diagnoses" Type="String"></asp:Parameter>
            <asp:Parameter Name="attending_physician_id" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="nursing_unit_id" Type="String"></asp:Parameter>
            <asp:Parameter Name="room" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="bed" Type="Int32"></asp:Parameter>
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter DbType="Date" Name="discharge_date"></asp:Parameter>
            <asp:Parameter Name="primary_diagnosis" Type="String"></asp:Parameter>
            <asp:Parameter Name="secondary_diagnoses" Type="String"></asp:Parameter>
            <asp:Parameter Name="attending_physician_id" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="nursing_unit_id" Type="String"></asp:Parameter>
            <asp:Parameter Name="room" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="bed" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="patient_id" Type="Int32"></asp:Parameter>
            <asp:Parameter DbType="Date" Name="admission_date"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

