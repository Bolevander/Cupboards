using System.Collections.Generic;


namespace CupBoards
{
    public abstract class GameStateController
    {
        #region Fields

        private readonly List<Controllers> _updateFeatures;
        private readonly List<Controllers> _fixedUpdateFeatures;
        private readonly List<Controllers> _lateUpdateFeatures;

#if UNITY_EDITOR
        private readonly List<Controllers> _onDrawGizmosFeatures = new List<Controllers>();
#endif

        #endregion


        #region ClassLifeCycles

        protected GameStateController(int capacity = 8)
        {
            _updateFeatures = new List<Controllers>(capacity);
            _fixedUpdateFeatures = new List<Controllers>(capacity);
            _lateUpdateFeatures = new List<Controllers>(capacity);
        }

        #endregion


        #region Methods

        public void Initialize()
        {
            foreach (Controllers feature in _updateFeatures)
            {
                feature.Initialize();
            }

            foreach (Controllers feature in _fixedUpdateFeatures)
            {
                feature.Initialize();
            }

            foreach (Controllers feature in _lateUpdateFeatures)
            {
                feature.Initialize();
            }

#if UNITY_EDITOR
            foreach (Controllers feature in _onDrawGizmosFeatures)
            {
                feature.Initialize();
            }
#endif
        }

        public void Execute(UpdateType updateType)
        {
            List<Controllers> features = null;
            switch (updateType)
            {
                case UpdateType.Update:
                    features = _updateFeatures;
                    break;

                case UpdateType.Fixed:
                    features = _fixedUpdateFeatures;
                    break;

                case UpdateType.Late:
                    features = _lateUpdateFeatures;
                    break;

#if UNITY_EDITOR
                case UpdateType.Gizmos:
                    features = _onDrawGizmosFeatures;
                    break;
#endif

                default:
                    break;
            }

            foreach (Controllers feature in features)
            {
                feature.Execute();
            }
        }

        public void TearDown()
        {
            foreach (Controllers feature in _updateFeatures)
            {
                feature.TearDown();
            }

            foreach (Controllers feature in _fixedUpdateFeatures)
            {
                feature.TearDown();
            }

            foreach (Controllers feature in _lateUpdateFeatures)
            {
                feature.TearDown();
            }

#if UNITY_EDITOR
            foreach (Controllers feature in _onDrawGizmosFeatures)
            {
                feature.TearDown();
            }
#endif
        }
        protected void AddUpdateFeature(Controllers controller)
        {
            _updateFeatures.Add(controller);
        }


        protected void AddFixedUpdateFeature(Controllers controller)
        {
            _fixedUpdateFeatures.Add(controller);
        }

        protected void AddLateUpdateFeature(Controllers controller)
        {
            _lateUpdateFeatures.Add(controller);
        }

#if UNITY_EDITOR

        protected void AddOnDrawGizmosFeature(Controllers controller)
        {
            _onDrawGizmosFeatures.Add(controller);
        }

#endif

        #endregion
    }
}
