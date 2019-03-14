using System;

namespace Librarian.DB
{
    /// <summary>
    /// Средство доступа к строке подключения (прототип обратного вызова)
    /// </summary>
    public delegate string ConnectionStringAccessorCallback();
}
