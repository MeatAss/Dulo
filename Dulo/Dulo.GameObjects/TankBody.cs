using Dulo.BaseModels;
using Dulo.BaseModels.SettingsModels;


namespace Dulo.GameObjects
{
    public class TankBody : BaseAnimationModel
    {
        private float speedMoving;
        private float speedRotating;

        public float SpeedMoving
        {
            get
            {
                return speedMoving;
            }
            set
            {
                speedMoving = value < 0 ? 0 : value;
            }
        }

        public float SpeedRotating
        {
            get
            {
                return speedRotating;
            }
            set
            {
                speedRotating = value < 0 ? 0 : value;
            }
        }

        public TankBody(SettingBaseAnimationModel settingBaseAnimationModel) : base(settingBaseAnimationModel)
        {
            Body.Mass = 20f;
        }
    }
}
