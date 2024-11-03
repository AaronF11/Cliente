# Cliente UDP

Este proyecto implementa un cliente UDP que se conecta a un servidor para obtener la fecha y hora actuales. El cliente envía una solicitud al servidor cada 5 segundos y muestra la respuesta en la consola.

## Descripción del Cliente

El cliente UDP está diseñado para enviar solicitudes de fecha y hora al servidor y recibir las respuestas correspondientes. Si no se recibe respuesta en el tiempo especificado (5000 ms), se mostrará un mensaje de error.

### Características

- Envía una solicitud al servidor cada **5 segundos**.
- Muestra la respuesta del servidor en la consola.
- Maneja excepciones relacionadas con el tiempo de espera y errores de conexión.
- Utiliza la biblioteca **Spectre.Console** para una presentación visual atractiva en la consola.

### Ejecución del Cliente

1. Asegúrate de tener el SDK de .NET instalado en tu máquina.
2. Compila y ejecuta la clase `Cliente`:

```bash
dotnet run --project Cliente.csproj
