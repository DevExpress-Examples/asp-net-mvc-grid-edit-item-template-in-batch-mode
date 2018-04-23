Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Linq
Imports System.Web

Namespace Models
	Public Class BatchEditRepository
		Public Shared ReadOnly Property GridData() As List(Of GridDataItem)
			Get
				Dim key = "34FAA431-CF79-4869-9488-93F6AAE81263"
				Dim Session = HttpContext.Current.Session
				If Session(key) Is Nothing Then
					Session(key) = Enumerable.Range(0, 100).Select(Function(i) New GridDataItem With {.ID = i, .C1 = i Mod 2, .C2 = i * 0.5 Mod 3, .C3 = "C3 " & i, .C4 = i Mod 2 = 0, .C5 = New DateTime(2013 + i, 12, 16)}).ToList()
				End If
				Return CType(Session(key), List(Of GridDataItem))
			End Get
		End Property
		Public Shared Function InsertNewItem(ByVal postedItem As GridDataItem) As GridDataItem
			Dim newItem = New GridDataItem() With {.ID = GridData.Count}
			LoadNewValues(newItem, postedItem)
			GridData.Add(newItem)
			Return newItem
		End Function
		Public Shared Function UpdateItem(ByVal postedItem As GridDataItem) As GridDataItem
			Dim editedItem = GridData.First(Function(i) i.ID = postedItem.ID)
			LoadNewValues(editedItem, postedItem)
			Return editedItem
		End Function
		Public Shared Function DeleteItem(ByVal itemKey As Integer) As GridDataItem
			Dim item = GridData.First(Function(i) i.ID = itemKey)
			GridData.Remove(item)
			Return item
		End Function
		Protected Shared Sub LoadNewValues(ByVal newItem As GridDataItem, ByVal postedItem As GridDataItem)
			newItem.C1 = postedItem.C1
			newItem.C2 = postedItem.C2
			newItem.C3 = postedItem.C3
			newItem.C4 = postedItem.C4
			newItem.C5 = postedItem.C5
		End Sub
	End Class

	Public Class GridDataItem
		Private privateID As Integer
		Public Property ID() As Integer
			Get
				Return privateID
			End Get
			Set(ByVal value As Integer)
				privateID = value
			End Set
		End Property
		Private privateC1 As Integer
		Public Property C1() As Integer
			Get
				Return privateC1
			End Get
			Set(ByVal value As Integer)
				privateC1 = value
			End Set
		End Property
		Private privateC2 As Double
		Public Property C2() As Double
			Get
				Return privateC2
			End Get
			Set(ByVal value As Double)
				privateC2 = value
			End Set
		End Property
		Private privateC3 As String
		Public Property C3() As String
			Get
				Return privateC3
			End Get
			Set(ByVal value As String)
				privateC3 = value
			End Set
		End Property
		Private privateC4 As Boolean
		Public Property C4() As Boolean
			Get
				Return privateC4
			End Get
			Set(ByVal value As Boolean)
				privateC4 = value
			End Set
		End Property
		Private privateC5 As DateTime
		Public Property C5() As DateTime
			Get
				Return privateC5
			End Get
			Set(ByVal value As DateTime)
				privateC5 = value
			End Set
		End Property
	End Class
End Namespace