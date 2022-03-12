using System;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Mssc.Services.ConnectionManagement
{

    class TestIPAddress
    {

        /**
          * Метод IPAddresses получает информацию об IP-адресе выбранного сервера.
          * Затем он отображает тип семейства адресов, поддерживаемый сервером, и его
          * IP-адрес в стандартном и байтовом формате.
          **/
        private static void IPAddresses(string server)
        {
            try
            {
                System.Text.ASCIIEncoding ASCII = new System.Text.ASCIIEncoding();

                // Получить информацию о сервере.
                IPHostEntry heserver = Dns.GetHostEntry(server);

                // Цикл в списке адресов
                foreach (IPAddress curAdd in heserver.AddressList)
                {


                    // Отображение типа семейства адресов, поддерживаемого сервером. Если
                    // сервер поддерживает IPv6, это значение: InterNetworkV6. Если сервер
                    // также с поддержкой IPv4 будет дополнительное значение InterNetwork.
                    Console.WriteLine("AddressFamily: " + curAdd.AddressFamily.ToString());

                    // Отображение свойства ScopeId в случае адресов IPV6.
                    if (curAdd.AddressFamily.ToString() == ProtocolFamily.InterNetworkV6.ToString())
                        Console.WriteLine("Scope Id: " + curAdd.ScopeId.ToString());


                    // Отображение IP-адреса сервера в стандартном формате. В
                    // IPv4 формат будет четырехкратно-точечным, в IPv6 это будет
                    // in в двоеточие-шестнадцатеричном представлении.
                    Console.WriteLine("Address: " + curAdd.ToString());

                    // Отображение IP-адреса сервера в байтовом формате.
                    Console.Write("AddressBytes: ");

                    Byte[] bytes = curAdd.GetAddressBytes();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        Console.Write(bytes[i]);
                    }

                    Console.WriteLine("\r\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[DoResolve] Exception: " + e.ToString());
            }
        }

        // Этот IPAddressAdditionalInfo отображает дополнительную информацию об адресе сервера.
        private static void IPAddressAdditionalInfo()
        {
            try
            {
                // Отображение флагов, которые показывают, поддерживает ли сервер IPv4 или IPv6.
                // адресные схемы.
                Console.WriteLine("\r\nSupportsIPv4: " + Socket.SupportsIPv4);
                Console.WriteLine("SupportsIPv6: " + Socket.SupportsIPv6);

                if (Socket.SupportsIPv6)
                {
                    // Показать сервер Любой адрес. Этот IP-адрес указывает, что сервер
                    // должен прослушивать активность клиента на всех сетевых интерфейсах.
                    Console.WriteLine("\r\nIPv6Any: " + IPAddress.IPv6Any.ToString());

                    // Отображение адрес обратной связи сервера.
                    Console.WriteLine("IPv6Loopback: " + IPAddress.IPv6Loopback.ToString());

                    // Используется на первом этапе автоконфигурации.
                    Console.WriteLine("IPv6None: " + IPAddress.IPv6None.ToString());

                    Console.WriteLine("IsLoopback(IPv6Loopback): " + IPAddress.IsLoopback(IPAddress.IPv6Loopback));
                }
                Console.WriteLine("IsLoopback(Loopback): " + IPAddress.IsLoopback(IPAddress.Loopback));
            }
            catch (Exception e)
            {
                Console.WriteLine("[IPAddresses] Exception: " + e.ToString());
            }
        }

        public static void Main(string[] args)
        {
            string server = null;

            // Определите регулярное выражение для анализа ввода пользователя.
            // Это проверка безопасности. Это позволяет только
            // буквенно-цифровая входная строка длиной от 2 до 40 символов.
            Regex rex = new Regex(@"^[a-zA-Z]\w{1,39}$");

            if (args.Length < 1)
            {
                // Если в качестве аргумента этой программе не передается имя сервера, используйте текущий
                // имя сервера по умолчанию.
                server = Dns.GetHostName();
                Console.WriteLine("Using current host: " + server);
            }
            else
            {
                server = args[0];
                if (!(rex.Match(server)).Success)
                {
                    Console.WriteLine("Input string format not allowed.");
                    return;
                }
            }

            // Получите список адресов, связанных с запрошенным сервером.
            IPAddresses(server);

            // Получить дополнительную информацию об адресе.ddressAdditionalInfo();
        }
    }
}