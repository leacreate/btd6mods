using System;
using ModHelper;
using HarmonyLib;

public class ProjectileMultiplier {
    private static int multiplier = 1;
    private static bool isDuplicating = false;

    public static void OnF7KeyPress() {
        ModHelper.Input.ShowTextInput("Enter projectile multiplier:", "1", OnInputReceived);
    }

    private static void OnInputReceived(string input) {
        if (int.TryParse(input, out int result)) {
            multiplier = result;
        }
    }
}

[HarmonyPatch(typeof(ProjectileManager), "SpawnProjectile")]
public class ProjectileManagerPatch {
    static void Prefix(Projectile projectile) {
        if (isDuplicating) return;

        if (multiplier > 1) {
            isDuplicating = true;
            for (int i = 0; i < multiplier - 1; i++) {
                // Duplicate the projectile spawn
                ProjectileManager.Instance.SpawnProjectile(projectile);
            }
            isDuplicating = false;
        }
    }
}
