Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports DevExpress.Web.Mvc
Imports Models

Namespace GridViewBatchEdit.Controllers
	Public Class HomeController
		Inherits Controller

		Public Function Index() As ActionResult
			Return View()
		End Function

		<ValidateInput(False)>
		Public Function GridViewPartial() As ActionResult
			Return PartialView("_GridViewPartial", BatchEditRepository.GridData)
		End Function

		<HttpPost, ValidateInput(False)>
		Public Function BatchUpdatePartial(ByVal batchValues As MVCxGridViewBatchUpdateValues(Of GridDataItem, Integer)) As ActionResult
			If ModelState.IsValid Then
				Try
					For Each item In batchValues.Insert
						If batchValues.IsValid(item) Then
							BatchEditRepository.InsertNewItem(item)
						End If
					Next item
					For Each item In batchValues.Update
						If batchValues.IsValid(item) Then
							BatchEditRepository.UpdateItem(item)
						End If
					Next item
					For Each itemKey In batchValues.DeleteKeys
						BatchEditRepository.DeleteItem(itemKey)
					Next itemKey
				Catch e As Exception
					ViewData("EditError") = e.Message
				End Try
			Else
				ViewData("EditError") = "Please, correct all errors."
			End If
			Return PartialView("_GridViewPartial", BatchEditRepository.GridData)
		End Function
	End Class
End Namespace