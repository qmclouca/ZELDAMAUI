using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class GameLoopService
    {
        private readonly GraphicsView _view;
        private readonly GameDrawable _drawable;

        public GameLoopService(GraphicsView view, GameDrawable drawable)
        {
            _view = view;
            _drawable = drawable;
        }

        public void Start()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                _view.Invalidate();
                return true; // Continua o timer
            });
        }
    }

}
