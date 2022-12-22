using Evergine.Framework;

namespace EvergineTest
{
    public class MyScene : Scene
    {
        public override void RegisterManagers()
        {
            base.RegisterManagers();
            this.Managers.AddManager(new global::Evergine.Bullet.BulletPhysicManager3D());
        }

        protected override void CreateScene()
        {
        }
    }
}