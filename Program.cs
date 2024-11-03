using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

class Cliente
{
    private const string ServerIp = "127.0.0.1"; // Dirección IP del servidor
    private const int ServerPort = 12345; // Puerto del servidor

    /// <summary>
    /// Método principal que inicia el cliente UDP.
    /// </summary>
    public static async Task Main()
    {
        Console.Title = "Cliente"; // Establece el título de la ventana de la consola

        // Muestra el título "Cliente" en la consola con estilo
        AnsiConsole.Write(new FigletText("Cliente")
            .Centered()
            .Color(Color.Green));

        // Crear un cliente UDP
        UdpClient client = new UdpClient();

        // Configurar el punto de conexión del servidor
        IPEndPoint serverEndpoint = new IPEndPoint(IPAddress.Parse(ServerIp), ServerPort);
        byte[] requestBytes = Encoding.UTF8.GetBytes("Solicitud de fecha y hora"); // Mensaje de solicitud

        while (true) // Bucle infinito para enviar solicitudes
        {
            try
            {
                // Enviar la solicitud al servidor
                await client.SendAsync(requestBytes, requestBytes.Length, serverEndpoint);
                AnsiConsole.MarkupLine("[yellow]Solicitud enviada al servidor {0}:{1}[/]", ServerIp, ServerPort);

                // Configurar el tiempo de espera para recibir la respuesta
                client.Client.ReceiveTimeout = 5000;

                // Esperar la respuesta del servidor
                UdpReceiveResult result = await client.ReceiveAsync();

                // Convertir la respuesta de bytes a cadena
                string response = Encoding.UTF8.GetString(result.Buffer);
                AnsiConsole.MarkupLine("[green bold]Respuesta recibida:[/] [bold cyan]{0}[/]", response);
            }
            catch (SocketException ex)
            {
                // Manejo de errores de socket (tiempo de espera o errores de conexión)
                if (ex.SocketErrorCode == SocketError.TimedOut)
                {
                    AnsiConsole.MarkupLine("[red]Error: No se recibió respuesta en el tiempo de espera de 5000 ms.[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Error de socket: {0}[/]", ex.Message);
                }
            }
            catch (Exception ex)
            {
                // Manejo de otros errores
                AnsiConsole.MarkupLine("[red]Error inesperado: {0}[/]", ex.Message);
            }

            await Task.Delay(5000); // Esperar 5 segundos antes de enviar la siguiente solicitud
        }

        // Se puede cerrar el cliente si se llega a un punto de salida en el futuro.
        // client.Close(); // Descomentar cuando decidas cerrar el cliente en un futuro.
    }
}
