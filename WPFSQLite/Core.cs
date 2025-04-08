namespace WPFSQLite
{
    class Core
    {
        private static WpfContext _context;

        public static WpfContext GetContext()
        {
            if (_context == null)
            {
                _context = new WpfContext();
            }
            return _context;
        }
    }
}
