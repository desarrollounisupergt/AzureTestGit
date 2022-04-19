Partial Class DataSet1
    Partial Class productosDataTable

        Private Sub productosDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.IdSubcategoriaColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class
