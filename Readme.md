<!-- default file list -->
*Files to look at*:

* [HomeController.cs](./CS/GridViewBatchEdit/Controllers/HomeController.cs) (VB: [HomeController.vb](./VB/GridViewBatchEdit/Controllers/HomeController.vb))
* [Model.cs](./CS/GridViewBatchEdit/Models/Model.cs) (VB: [Model.vb](./VB/GridViewBatchEdit/Models/Model.vb))
* [AjaxLogin.js](./CS/GridViewBatchEdit/Scripts/AjaxLogin.js) (VB: [AjaxLogin.js](./VB/GridViewBatchEdit/Scripts/AjaxLogin.js))
* [_GridViewPartial.cshtml](./CS/GridViewBatchEdit/Views/Home/_GridViewPartial.cshtml)
* **[Index.cshtml](./CS/GridViewBatchEdit/Views/Home/Index.cshtml)**
<!-- default file list end -->
# GridView - Batch Editing - A simple implementation of an EditItem template


<p>This example demonstrates how to create a custom editor inside column's DataItem template when GridView is in Batch Edit mode.<br><br></p>
<p>You can implement the EditItem template for a column by performing the following steps:<br><br>1. Specify column's EditItem template:</p>


```cs
        settings.Columns.Add(column =>
        {
            column.FieldName = "C1";
            column.SetEditItemTemplateContent(c =>
            {
                @Html.DevExpress().SpinEdit(spinSettings =>
                {
                    spinSettings.Name = "C1spinEdit";

                    spinSettings.Width = System.Web.UI.WebControls.Unit.Percentage(100);
                    spinSettings.Properties.ClientSideEvents.KeyDown = "C1spinEdit_KeyDown";
                    spinSettings.Properties.ClientSideEvents.LostFocus = "C1spinEdit_LostFocus";
                }).Render();
            });
        });

```


<p>2. Handle grid's client-side <a href="https://help.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_BatchEditStartEditingtopic">BatchEditStartEditing</a> event to set the grid's cell values to the editor. It is possible to get the focused cell value using the <a href="https://help.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridViewBatchEditStartEditingEventArgs_rowValuestopic">e.rowValues</a> property:</p>


```js
        function Grid_BatchEditStartEditing(s, e) {
            var templateColumn = s.GetColumnByField("C1");
            if (!e.rowValues.hasOwnProperty(templateColumn.index))
                return;
            var cellInfo = e.rowValues[templateColumn.index];
            C1spinEdit.SetValue(cellInfo.value);
            if (e.focusedColumn === templateColumn)
                C1spinEdit.SetFocus();
        }

```


<p><br>3. Handle the <a href="https://help.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_BatchEditEndEditingtopic">BatchEditEndEditing</a> event to pass the value entered in the editor to the grid's cell:</p>


```js
        function Grid_BatchEditEndEditing(s, e) {
            var templateColumn = s.GetColumnByField("C1");
            if (!e.rowValues.hasOwnProperty(templateColumn.index))
                return;
            var cellInfo = e.rowValues[templateColumn.index];
            cellInfo.value = C1spinEdit.GetValue();
            cellInfo.text = C1spinEdit.GetText();
            C1spinEdit.SetValue(null);
        }

```


<p> </p>
<p>4. The <a href="https://help.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_BatchEditRowValidatingtopic">BatchEditRowValidating</a> event allows validating the grid's cell based on the entered value:</p>


```js
       function Grid_BatchEditRowValidating(s, e) {
            var templateColumn = s.GetColumnByField("C1");
            var cellValidationInfo = e.validationInfo[templateColumn.index];
            if (!cellValidationInfo) return;
            var value = cellValidationInfo.value;
            if (!ASPxClientUtils.IsExists(value) || ASPxClientUtils.Trim(value) === "") {
                cellValidationInfo.isValid = false;
                cellValidationInfo.errorText = "C1 is required";
            }
        }

```


<p> <br>5. Finally, handle the editor's client-side <a href="https://documentation.devexpress.com/AspNet/DevExpressWebASPxEditorsScriptsASPxClientTextEdit_KeyDowntopic.aspx">KeyDown</a> and <a href="https://documentation.devexpress.com/AspNet/DevExpressWebASPxEditorsScriptsASPxClientEdit_LostFocustopic.aspx">LostFocus</a> events to emulate the behavior of standard grid editors when an end-user uses a keyboard or mouse:</p>


```js
        var preventEndEditOnLostFocus = false;
        function C1spinEdit_KeyDown(s, e) {
            var keyCode = ASPxClientUtils.GetKeyCode(e.htmlEvent);
            if (keyCode === ASPx.Key.Esc) {
                var cellInfo = grid.batchEditApi.GetEditCellInfo();
                window.setTimeout(function () {
                    grid.SetFocusedCell(cellInfo.rowVisibleIndex, cellInfo.column.index);
                }, 0);
                s.GetInputElement().blur();
                return;
            }
            if (keyCode !== ASPx.Key.Tab && keyCode !== ASPx.Key.Enter) return;
            var moveActionName = e.htmlEvent.shiftKey ? "MoveFocusBackward" : "MoveFocusForward";
            if (grid.batchEditApi[moveActionName]()) {
                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
                preventEndEditOnLostFocus = true;
            }
        }
        function C1spinEdit_LostFocus(s, e) {
            if (!preventEndEditOnLostFocus)
                grid.batchEditApi.EndEdit();
            preventEndEditOnLostFocus = false;
        } 
```


<p> <br><strong>See Also:<br></strong><a href="https://www.devexpress.com/Support/Center/p/T115096">ASPxGridView - Batch Editing - A simple implementation of an EditItem template</a> <br><a href="https://www.devexpress.com/Support/Center/p/T166450">GridView - Batch Editing - A simple implementation of an EditItemTemplate with client-side unobtrusive validation </a></p>

<br/>


