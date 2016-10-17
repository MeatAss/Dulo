using Dulo.BaseModels;
using Dulo.BaseModels.SettingsModels;

namespace Dulo.GameObjects
{
    public class Track : BaseAnimationModel
    {
        public Track(SettingBaseAnimationModel settingBaseAnimationModel) : base(settingBaseAnimationModel)
        {
            //Body.Mass = 0.0000001f;
            Body.AngularDamping = 30f;
        }
    }
}
