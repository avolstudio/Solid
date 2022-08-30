using Solid.Model;

/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid.UI
{
    public abstract class SolidObserver<TModel> : PopUp where TModel : IModel<TModel>
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

        private void OnDestroy()
        {
            Unsubscribe();
        }

        public void ApplyModel(TModel model)
        {
            Model = model;

            OnModelChanged(Model);
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