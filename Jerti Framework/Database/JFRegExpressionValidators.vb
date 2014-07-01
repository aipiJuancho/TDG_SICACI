Namespace Database
    Public Class JFRegExpValidators
        Public Const DUI As String = "^[0-9]{8}\-\d$"
        Public Const [Date] As String = "^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$"
        Public Const Telefono As String = "^(2|7)\d{3}\-\d{4}$"
        Public Const Email As String = "^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$"
        Public Const OnlyLetras As String = "^[a-zA-ZáéíóúÁÉÍÓÚ]+$"
        Public Const OnlyLetrasWithSpace As String = "^[a-zA-ZáéíóúÁÉÍÓÚ\ ]+$"
        Public Const RequiredTwoApellidos As String = "^[a-zA-ZáéíóúÁÉÍÓÚ]+\ [a-zA-ZáéíóúÁÉÍÓÚ]+$"
        Public Const OnlyEnteros As String = "^\d+$"
        Public Const Year As String = "^20\d{2}$"
        Public Const Hora As String = "^(0[1-9]|1[0-2])\:[0-5][0-9]\ (a\.m\.|p\.m\.)$"
        Public Const UserSystem As String = "^[a-zA-Z\.]+$"
    End Class
End Namespace
