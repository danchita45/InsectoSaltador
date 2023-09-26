using InsectoSaltador;
using System;
class program {
    public static Random random = new Random();
    static void Main(string[] args)
    {
        CruceGeneral();
    }

    static void CruceGeneral()
    {
        int poblacionSize = 16; // Tamaño de la población
        int generaciones = 10; // Número de generaciones
        double tasaMutacion = 0.1; // Tasa de mutación

        List<Insecto> poblacion = new List<Insecto>();

        // Inicializa la población con insectos aleatorios
        for (int i = 0; i < poblacionSize; i++)
        {
            Insecto insectoAleatorio = GenerarInsectoAleatorio();
            poblacion.Add(insectoAleatorio);
        }

        for (int generacion = 0; generacion < generaciones; generacion++)
        {
            // Calcula la aptitud de cada insecto en la población
            Dictionary<Insecto, double> aptitudes = new Dictionary<Insecto, double>();
            foreach (Insecto insecto in poblacion)
            {
                double aptitud = FuncionDeAptitud(insecto);
                aptitudes.Add(insecto, aptitud);
            }

            // Ordena la población por aptitud (ascendente)
            var poblacionOrdenada = poblacion.OrderBy(i => aptitudes[i]).ToList();

            // Selecciona los dos mejores candidatos
            Insecto mejor1 = poblacionOrdenada[0];
            Insecto mejor2 = poblacionOrdenada[1];

            // Realiza cruces y mutaciones para generar la nueva población
            List<Insecto> nuevaPoblacion = new List<Insecto>();
            int crucesRealizados = 0; // Contador de cruces realizados

            for (int i = 0; i < poblacionSize; i += 4)
            {
                Insecto hijo1 = Cruzar(mejor1, mejor2);
                Insecto hijo2 = Cruzar(mejor1, mejor2);
                Insecto hijo3 = Cruzar(mejor1, mejor2);
                Insecto hijo4 = Cruzar(mejor1, mejor2);

                // Aplica mutación a los hijos
                hijo1 = Mutar(hijo1, tasaMutacion);
                hijo2 = Mutar(hijo2, tasaMutacion);
                hijo3 = Mutar(hijo3, tasaMutacion);
                hijo4 = Mutar(hijo4, tasaMutacion);

                nuevaPoblacion.Add(hijo1);
                nuevaPoblacion.Add(hijo2);
                nuevaPoblacion.Add(hijo3);
                nuevaPoblacion.Add(hijo4);

                crucesRealizados += 2; // Cada pareja de padres produce 2 hijos
            }

            // Reemplaza la población antigua con la nueva
            poblacion = nuevaPoblacion;

            // Calcula las características del hijo más apto
            Insecto hijoMasApto = poblacionOrdenada[0];

            // Calcula la altura de salto del hijo más apto
            double alturaDeSaltoHijoMasApto = CalcularAlturaDeSalto(hijoMasApto);

            // Imprime los resultados de la generación, incluyendo la altura de salto del hijo más apto
            Console.WriteLine($"Generación {generacion + 1}");
            Console.WriteLine($"Cruces realizados: {crucesRealizados}");
            Console.WriteLine($"Características del mejor padre:  Masa->{mejor1.Masa} Patas->{mejor1.Patas}" +
                $" ");
            Console.WriteLine($"Características del segundo mejor padre: Masa->{mejor2.Masa} Patas->{mejor2.Patas}");
            Console.WriteLine($"Características del hijo más apto: Masa ->{hijoMasApto.Masa} Patas ->{hijoMasApto.Patas}");
            Console.WriteLine($"Altura de salto del hijo más apto: {alturaDeSaltoHijoMasApto}");
            Console.WriteLine();
        }
    }

    static Insecto Mutar(Insecto insecto, double tasaMutacion)
    {
        Insecto insectoMutado = new Insecto(insecto.Patas, insecto.cabeza, insecto.longCabeza, insecto.Masa);

        // Recorre cada atributo y decide si aplicar una mutación basada en la tasa de mutación
        if (random.NextDouble() < tasaMutacion)
        {
            // Aplica una mutación aleatoria a uno de los atributos
            int atributoAMutar = random.Next(4); // Selecciona aleatoriamente uno de los atributos (0 a 3)

            switch (atributoAMutar)
            {
                case 0:
                    // Mutar el número de patas
                    insectoMutado.Patas = random.Next(1, 11); // Puedes ajustar el rango según tus necesidades
                    break;
                case 1:
                    // Mutar la cabeza
                    insectoMutado.cabeza = random.Next(1, 11); // Puedes ajustar el rango según tus necesidades
                    break;
                case 2:
                    // Mutar la longitud de la cabeza
                    insectoMutado.longCabeza = random.Next(1, 11); // Puedes ajustar el rango según tus necesidades
                    break;
                case 3:
                    // Mutar la masa
                    insectoMutado.Masa = random.Next(1, 11); // Puedes ajustar el rango según tus necesidades
                    break;
            }
        }

        return insectoMutado;
    }

    static Insecto Cruzar(Insecto padre1, Insecto padre2)
    {
        // Obtenemos las características de los padres.
        int patasPadre1 = padre1.Patas;
        int patasPadre2 = padre2.Patas;
        int cabezaPadre1 = padre1.cabeza;
        int cabezaPadre2 = padre2.cabeza;
        int longCabezaPadre1 = padre1.longCabeza;
        int longCabezaPadre2 = padre2.longCabeza;
        int masaPadre1 = padre1.Masa;
        int masaPadre2 = padre2.Masa;

        // Realizamos el cruce de características de alguna manera, por ejemplo, intercambiando valores.
        int patasHijo = (patasPadre1 + patasPadre2) / 2; // Promedio de patas de los padres.
        int cabezaHijo = (cabezaPadre1 + cabezaPadre2) / 2; // Promedio de cabeza de los padres.
        int longCabezaHijo = (longCabezaPadre1 + longCabezaPadre2) / 2; // Promedio de longitud de cabeza de los padres.
        int masaHijo = (masaPadre1 + masaPadre2) / 2; // Promedio de masa de los padres.

        // Creamos un nuevo insecto (hijo) con las características resultantes y lo devolvemos.
        Insecto hijo = new Insecto()
        {
            Patas = patasHijo,
            cabeza = cabezaHijo,
            longCabeza = longCabezaHijo,
            Masa = masaHijo
        };

        return hijo;
    }


    static double FuncionDeAptitud(Insecto insecto)
    {
        // La función de aptitud evalúa la altura de salto del insecto.
        double alturaDeSalto = CalcularAlturaDeSalto(insecto);

        // Calcula la diferencia entre la altura de salto y la altura objetivo (10 cm).
        double diferencia = Math.Abs(alturaDeSalto - 50.0);

        // Si la altura de salto es igual o mayor que 10 cm, la aptitud es máxima (0).
        if (alturaDeSalto >= 50)
        {
            return 0.0;
        }
        else
        {
            // Cuanto mayor sea la diferencia, menor será la aptitud.
            double aptitud = 1.0 / (1.0 + diferencia);
            return aptitud;
        }
    }
        
    static double CalcularAlturaDeSalto(Insecto insecto)
    {
        double altura = insecto.Patas * 2.0 / insecto.longCabeza * insecto.Masa;

        return altura;

    }
    static Insecto GenerarInsectoAleatorio()
    {
        // Genera valores aleatorios para las características del insecto
        int patas = random.Next(1, 11);
        int cabeza = random.Next(1, 11);
        int longCabeza = random.Next(1, 11);
        int masa = random.Next(1, 11);

        return new Insecto(patas, cabeza, longCabeza, masa);
    }

}



