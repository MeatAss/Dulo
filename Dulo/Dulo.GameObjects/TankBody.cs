using Dulo.BaseModels;
using Dulo.BaseModels.SettingsModels;


namespace Dulo.GameObjects
{
    public class TankBody : BaseAnimationModel
    {
        private float speedMoving;
        private float speedRotation;

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

        public float SpeedRotation
        {
            get
            {
                return speedRotation;
            }
            set
            {
                speedRotation = value < 0 ? 0 : value;
            }
        }

        public TankBody(SettingBaseAnimationModel settingBaseAnimationModel) : base(settingBaseAnimationModel)
        {
            Body.Mass = 20f;
        }
    }
}
