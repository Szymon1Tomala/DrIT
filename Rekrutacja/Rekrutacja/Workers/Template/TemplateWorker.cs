using Soneta.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using Soneta.Kadry;
using Soneta.Types;
using Rekrutacja.Workers.Template;
using Rekrutacja.Calculator;
//using static Soneta.Business.FieldValue;

//Rejetracja Workera - Pierwszy TypeOf określa jakiego typu ma być wyświetlany Worker, Drugi parametr wskazuje na jakim Typie obiektów będzie wyświetlany Worker
[assembly: Worker(typeof(TemplateWorker), typeof(Pracownicy))]
namespace Rekrutacja.Workers.Template
{
    public class TemplateWorker
    {
        //Aby parametry działały prawidłowo dziedziczymy po klasie ContextBase
        public class TemplateWorkerParametry : ContextBase
        {
            public TemplateWorkerParametry(Context context) : base(context) { }

            [Caption("Data obliczeń")]
            public Date DataObliczen { get; set; }

            [Caption("A")]
            public int A { get; set; }

            [Caption("B")]
            public int B { get; set; }

            [Caption("Operacja")]
            public char OperationSign { get; set; }

        }
        //Obiekt Context jest to pudełko które przechowuje Typy danych, aktualnie załadowane w aplikacji
        //Atrybut Context pobiera z "Contextu" obiekty które aktualnie widzimy na ekranie
        [Context]
        public Context Cx { get; set; }
        //Pobieramy z Contextu parametry, jeżeli nie ma w Context Parametrów mechanizm sam utworzy nowy obiekt oraz wyświetli jego formatkę
        [Context]
        public TemplateWorkerParametry Parametry { get; set; }
        //Atrybut Action - Wywołuje nam metodę która znajduje się poniżej
        [Action("Kalkulator",
           Description = "Prosty kalkulator ",
           Priority = 10,
           Mode = ActionMode.ReadOnlySession,
           Icon = ActionIcon.Accept,
           Target = ActionTarget.ToolbarWithText)]
        public void WykonajAkcje()
        {
            //Włączenie Debug, aby działał należy wygenerować DLL w trybie DEBUG
            DebuggerSession.MarkLineAsBreakPoint();
            //Pobieranie danych z Contextu
            Pracownik pracownik = null;

            var parameters = this.Parametry;
            var employees = (IEnumerable<Pracownik>)this.Cx[typeof(IEnumerable<Pracownik>)];

            ValidateParameters(parameters);

            var operationResult = Calculator.Calculator
                .PerformOperation(parameters.A, parameters.B, GetArithmeticOperationFromChar(parameters.OperationSign));

            //Modyfikacja danych
            //Aby modyfikować dane musimy mieć otwartą sesję, któa nie jest read only
            using (Session nowaSesja = this.Cx.Login.CreateSession(false, false, "ModyfikacjaPracownika"))
            {
                //Otwieramy Transaction aby można było edytować obiekt z sesji
                using (ITransaction trans = nowaSesja.Logout(true))
                {
                    //Pobieramy obiekt z Nowo utworzonej sesji
                    var pracownikZSesja = nowaSesja.Get(pracownik);
                    //Features - są to pola rozszerzające obiekty w bazie danych, dzięki czemu nie jestesmy ogarniczeni to kolumn jakie zostały utworzone przez producenta

                    foreach (var employee in employees)
                    {
                        var employeeFromSession = nowaSesja.Get(employee);

                        employeeFromSession.Features["DataObliczen"] = parameters.DataObliczen;
                        employeeFromSession.Features["Wynik"] = operationResult;
                    }

                    //Zatwierdzamy zmiany wykonane w sesji
                    trans.CommitUI();
                }
                //Zapisujemy zmiany
                nowaSesja.Save();
            }
        }

        private ArithmeticOperation GetArithmeticOperationFromChar(char operationSign)
        {
            switch (operationSign)
            {
                case '+':
                    return ArithmeticOperation.Addition;
                case '-':
                    return ArithmeticOperation.Subtraction;
                case '*':
                    return ArithmeticOperation.Multiplication;
                case '/':
                    return ArithmeticOperation.Division;
                default:
                    throw new ArgumentException("Invalid operation character.");
            }
        }

        private void ValidateParameters(TemplateWorkerParametry parameters)
        {
            if (parameters is null)
            {
                throw new ArgumentException("Something went wrong. Try again");
            }

            char[] allowedOperationSigns = { '+', '-', '*', '/' };

            if (allowedOperationSigns.Contains(parameters.OperationSign) is false)
            {
                throw new ArgumentException($"Only listed characters are allowed for operation sign: {allowedOperationSigns}");
            }

            if (parameters.OperationSign == '/' && parameters.B == 0)
            {
                throw new ArgumentException("It's not allowed to divide by 0");
            }
        }
    }
}