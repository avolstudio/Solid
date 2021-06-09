using Solid.Behaviours;
using Solid.Model;

/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid.View
{
    public abstract class SolidObserver<TModel> : SolidBehaviour where TModel : IModel<TModel>
    {
        private TModel _model;

        public TModel Model
        {
            get => _model;
            set
            {
                Unsubscribe();

                _model = value;

                if (_model == null) return;

                Subscribe();
            }
        }

        public void ApplyModel(TModel model)
        {
            Model = model;

            OnModelChanged(Model);
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            if (_model == null) return;

            _model.ModelChanged += OnModelChanged;
        }
        
        private void Unsubscribe()
        {
            if (_model == null) return;

            _model.ModelChanged -= OnModelChanged;
        }
        
        protected abstract void OnModelChanged(TModel model);
    }
}