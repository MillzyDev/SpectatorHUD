/*
 *      SpectatorHUD
 *      Copyright (C) 2026  Millzy
 *
 *      This program is free software: you can redistribute it and/or modify
 *      it under the terms of the GNU Lesser General Public License as published by
 *      the Free Software Foundation, either version 3 of the License, or
 *      (at your option) any later version.
 *
 *      This program is distributed in the hope that it will be useful,
 *      but WITHOUT ANY WARRANTY; without even the implied warranty of
 *      MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *      GNU General Public License for more details.
 *
 *      You should have received a copy of the GNU Lesser General Public License
 *      along with this program.  If not, see https://www.gnu.org/licenses/.
 */

using Il2CppSLZ.Marrow;

namespace SpectatorHUD.Extensions;

public static class GunExtensions
{
    extension(Gun gun)
    {
        public int GetAmmoCount()
        {
            if (gun._magState != null)
            {
                return gun.chamberedCartridge != null ? gun._magState.AmmoCount + 1 : gun._magState.AmmoCount;
            }

            // Chambered cartridge is not included in the MagState's count
            return gun.chamberedCartridge != null ? 1 : 0;
        }
    }
}