using Soneta.Business;
using System;
using System.Collections.Generic;
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

            [Caption("Figura geometryczna")]
            public GeometricFigure GeometricFigure { get; set; }
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

            var parameters = this.Parametry;
            var employees = (IEnumerable<Pracownik>)this.Cx[typeof(IEnumerable<Pracownik>)];

            ValidateParameters(parameters);

            var calculatedArea = AreaCalculator.Calculate(parameters.A, parameters.B, parameters.GeometricFigure);

            //Modyfikacja danych
            //Aby modyfikować dane musimy mieć otwartą sesję, któa nie jest read only
            using (Session nowaSesja = this.Cx.Login.CreateSession(false, false, "ModyfikacjaPracownika"))
            {
                //Otwieramy Transaction aby można było edytować obiekt z sesji
                using (ITransaction trans = nowaSesja.Logout(true))
                {
                    foreach (var employee in employees)
                    {
                        var employeeFromSession = nowaSesja.Get(employee);

                        employeeFromSession.Features["DataObliczen"] = parameters.DataObliczen;
                        employeeFromSession.Features["Wynik"] = (double)calculatedArea;
                    }

                    //Zatwierdzamy zmiany wykonane w sesji
                    trans.CommitUI();
                }
                //Zapisujemy zmiany
                nowaSesja.Save();
            }
        }

        private void ValidateParameters(TemplateWorkerParametry parameters)
        {
            if (parameters is null)
            {
                throw new ArgumentException("Something went wrong. Try again");
            }

            if (parameters.A < 0 || parameters.B < 0)
            {
                throw new ArgumentException("Values can't be negative");
            }
        }
    }
}