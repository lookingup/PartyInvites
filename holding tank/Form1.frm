VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   3030
   ClientLeft      =   120
   ClientTop       =   450
   ClientWidth     =   4560
   LinkTopic       =   "Form1"
   ScaleHeight     =   3030
   ScaleWidth      =   4560
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdInvokeCaller 
      Caption         =   "Invoke Caller"
      Height          =   420
      Left            =   1575
      TabIndex        =   2
      Top             =   2430
      Width           =   1500
   End
   Begin VB.TextBox txtInput 
      Height          =   555
      Left            =   1440
      TabIndex        =   1
      Text            =   "txtInput"
      Top             =   540
      Width           =   1860
   End
   Begin VB.CommandButton cmdCheck 
      Caption         =   "Check!"
      Height          =   960
      Left            =   1620
      TabIndex        =   0
      Top             =   1350
      Width           =   1455
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub cmdCheck_Click()
  Select Case txtInput.Text
    Case "", "0"
      Call MsgBox("Empty text or zero")
  End Select
End Sub

Private Sub Caller()
  Dim inf As Integer
  Dim errInfo As String
  On Error GoTo ErrHndlrCaller1
  
  Call Callee
  'control returned without resetting error handler
  On Error GoTo 0
  On Error GoTo ErrHndlrCaller2
  inf = 1 / 0
ErrHndlrCaller1:
  errInfo = Err.Number & ", " & Err.Source & ", " & Err.Description & ", " & Err.HelpFile & ", " & Err.HelpContext
  If Mid$(errInfo, 1, 1) = "," Then errInfo = ""
  Call MsgBox("[ErrHndlrCaller1] An error occurred in Caller. " & errInfo)
  Exit Sub
  
ErrHndlrCaller2:
  Call MsgBox("[ErrHndlrCaller2] An error occurred in Caller.")
End Sub

Private Sub Callee()
  Dim inf
  On Error GoTo ErrHndlrCallee
  inf = 1 / 0
  Exit Sub
ErrHndlrCallee:
  Call MsgBox("An error occurred in Callee.")
  Err.Raise Err.Number, Err.Source, Err.Description, Err.HelpFile, Err.HelpContext
End Sub

Private Sub cmdInvokeCaller_Click()
  Call Caller
End Sub
