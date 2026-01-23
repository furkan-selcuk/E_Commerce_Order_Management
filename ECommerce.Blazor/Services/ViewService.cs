using System;

namespace ECommerce.Blazor.Services
{
    public class ViewService
    {
        public string ActiveView { get; private set; } = "Home";

        public event Action? OnChange;
        // aktif görünümü ayarlar
        public void SetView(string viewName)
        {
            if (ActiveView != viewName)
            {
                ActiveView = viewName;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
