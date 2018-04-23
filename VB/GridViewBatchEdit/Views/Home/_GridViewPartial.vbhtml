@Code
    Dim grid = Html.DevExpress().GridView(Sub(settings)
    
                                                  settings.Name = "grid"
                                                  settings.CallbackRouteValues = New With {.Controller = "Home", .Action = "GridViewPartial"}
                                                  settings.SettingsEditing.BatchUpdateRouteValues = New With {.Controller = "Home", .Action = "BatchUpdatePartial"}
                                                  settings.SettingsEditing.Mode = GridViewEditingMode.Batch

                                                  settings.CommandColumn.Visible = True
                                                  settings.CommandColumn.ShowDeleteButton = True
                                                  settings.CommandColumn.ShowNewButtonInHeader = True

                                                  settings.Width = 800
                                                  settings.KeyFieldName = "ID"

                                                  settings.Columns.Add(Sub(column)
        
                                                                               column.FieldName = "C1"
                                                                               column.Width = 100
                                                                               column.SetEditItemTemplateContent(Sub(c)
            
                                                                                                                         Html.DevExpress().SpinEdit(Sub(spinSettings)
                
                                                                                                                                                            spinSettings.Name = "C1spinEdit"

                                                                                                                                                            spinSettings.Width = System.Web.UI.WebControls.Unit.Percentage(100)
                                                                                                                                                            spinSettings.Properties.ClientSideEvents.KeyDown = "C1spinEdit_KeyDown"
                                                                                                                                                            spinSettings.Properties.ClientSideEvents.LostFocus = "C1spinEdit_LostFocus"
                                                                                                                                                    End Sub).Render()
                                                                                                                 End Sub)
                                                                       End Sub)

                                                  settings.Columns.Add(Sub(column)
                                                                               column.Width = 100
                                                                               column.FieldName = "C2"
                                                                               column.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                       End Sub)
                                                  settings.Columns.Add("C3").Width = 120
                                                  settings.Columns.Add(Sub(column)
        
                                                                               column.FieldName = "C4"
                                                                               column.ColumnType = MVCxGridViewColumnType.CheckBox
                                                                       End Sub)
                                                  settings.Columns.Add(Sub(column)
                                                                               column.Width = 150
                                                                               column.FieldName = "C5"
                                                                               column.ColumnType = MVCxGridViewColumnType.DateEdit
                                                                       End Sub)


                                                  settings.ClientSideEvents.BatchEditStartEditing = "Grid_BatchEditStartEditing"
                                                  settings.ClientSideEvents.BatchEditEndEditing = "Grid_BatchEditEndEditing"
                                                  settings.ClientSideEvents.BatchEditRowValidating = "Grid_BatchEditRowValidating"


                                                  settings.CellEditorInitialize = Sub(s, e)
        
                                                                                          Dim editor As ASPxEdit = CType(e.Editor, ASPxEdit)
                                                                                          editor.ValidationSettings.Display = Display.Dynamic

                                                                                  End Sub
                                          End Sub)
   If ViewData("EditError") IsNot Nothing Then
		grid.SetEditErrorText(CStr(ViewData("EditError")))
End If
End Code
@grid.Bind(Model).GetHtml()
