using System;
using System.Collections.Generic;
using Falcon.Core.Model.Loadouts;

namespace Falcon.Core.Builder
{
    public static class GearBuilderExtensions
    {
        public static string BuildUnitgear(this GearBuilder builder, IEnumerable<Loadout> loadouts)
        {
            return builder
                .AddFileCommentHeader()
                .AddUnitgearPrefix()
                .OpenTypeSelectionBlock()
                .AddLoadoutCases(loadouts)
                .CloseTypeSelectionBlock()
                .CreateOutput();
        }

        public static GearBuilder AddUnitgearPrefix(this GearBuilder builder)
        {
            return builder.NewLine()
                .AddCommand(GearBuilderResources.UnitgearPrefixArrayInit)
                .NewLine()
                .AddCommand(GearBuilderResources.UnitgearItemRemoval)
                .NewLine()
                .AddCommand(GearBuilderResources.UnitgearRandomizationDisabling);
        }

        public static GearBuilder AddFileCommentHeader(this GearBuilder builder)
        {
            return builder.AddCommand(GearBuilderResources.UnitgearFileNameLine)
                .AddCommand(string.Format(GearBuilderResources.GeneratedByComment,
                    DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss")));

        }

        public static GearBuilder OpenTypeSelectionBlock(this GearBuilder builder)
        {
            return builder.NewLine()
                .AddCommand(GearBuilderResources.UnitgearLoadoutSwitchOpening);
        }

        public static GearBuilder CloseTypeSelectionBlock(this GearBuilder builder)
        {
            return builder
                .AddCommand(GearBuilderResources.UnitgearLoadoutSwitchClosing);

        }

        private static GearBuilder OpenCaseBlock(this GearBuilder builder, string caseName)
        {
            return builder
                .NewLine()
                .AddCommand(String.Format(GearBuilderResources.UnitgearCaseOpening, caseName));

        }

        private static GearBuilder CloseCaseBlock(this GearBuilder builder)
        {
            return builder
                .AddCommand(GearBuilderResources.UnitgearCaseClosing);
        }

        private static GearBuilder AddUnitgearCase(this GearBuilder builder, Loadout loadout)
        {
            return builder
                .OpenCaseBlock(loadout.Name)
                .IncreaseIndent()
                .AddUnitgearCaseBody(loadout.ManEquipment)
                .DecreaseIndent()
                .CloseCaseBlock();
        }

        public static GearBuilder AddLoadoutCases(this GearBuilder builder, IEnumerable<Loadout> loadouts)
        {
            return builder.ApplyForEach(AddUnitgearCase, loadouts);
        }

        private static GearBuilder AddUnitgearCaseBody(this GearBuilder builder, ManEquipment equipment)
        {
            return builder
                .AddCommand("//Umundurowanie")
                .AddUniform(equipment.Uniform?.Classname)
                .AddVest(equipment.Vest?.Classname)
                .AddHeadgear(equipment.Headgear?.Classname)
                .AddGoggles(equipment.Goggles?.Classname)
                .AddBackpack(equipment.Backpack?.Classname)
                .AddCommand("//Przypisane przedmioty")
                .AddAssignedItems(equipment.AssignedItems)
                .AddCommand("//Broń i dodatki")
                .AddPrimaryWeapon(equipment.PrimaryWeapon)
                .AddSecondaryWeapon(equipment.SecondaryWeapon)
                .AddSidearm(equipment.Sidearm)
                .AddCommand("//Przedmioty i magazynki w mundurze")
                .AddUniformItems(equipment.Uniform)
                .AddCommand("//Przedmioty i magazynki w kamizelce")
                .AddVestItems(equipment.Vest)
                .AddCommand("//Przedmioty i magazynki w plecaku")
                .AddBackpackItems(equipment.Backpack);
        }

        private static GearBuilder AddPrimaryWeapon(this GearBuilder builder, Weapon primaryWeapon)
        {
            if (primaryWeapon == null)
                return builder;

            return builder
                .AddCommandWithParameters($"{GearBuilderResources.UnitFunc} {GearBuilderResources.AddWeaponCommand}", primaryWeapon.Classname)
                .ApplyForEach(
                    (current, attachment) =>
                        current.AddCommandWithParameters(
                            $"{GearBuilderResources.UnitFunc} {GearBuilderResources.AddPrimaryWeaponItemCommand}",
                            attachment.Classname), primaryWeapon.Attachments)
                .AddCommandWithParameters(
                    $"{GearBuilderResources.UnitFunc} {GearBuilderResources.AddPrimaryWeaponItemCommand}",
                    primaryWeapon.Magazine?.Classname)
                .NewLine();
        }

        private static GearBuilder AddSecondaryWeapon(this GearBuilder builder, Weapon secondaryWeapon)
        {
            if (secondaryWeapon == null)
                return builder;

            return builder
                .AddCommandWithParameters($"{GearBuilderResources.UnitFunc} {GearBuilderResources.AddWeaponCommand}", secondaryWeapon.Classname)
                .ApplyForEach(
                    (current, attachment) =>
                        current.AddCommandWithParameters(
                            $"{GearBuilderResources.UnitFunc} {GearBuilderResources.AddSecondaryWeaponItemCommand}",
                            attachment.Classname), secondaryWeapon.Attachments)
                .AddCommandWithParameters(
                    $"{GearBuilderResources.UnitFunc} {GearBuilderResources.AddSecondaryWeaponItemCommand}",
                    secondaryWeapon.Magazine?.Classname)
                .NewLine();
        }

        private static GearBuilder AddSidearm(this GearBuilder builder, Weapon sidearm)
        {
            if (sidearm == null)
                return builder;

            return builder
                .AddCommandWithParameters($"{GearBuilderResources.UnitFunc} {GearBuilderResources.AddWeaponCommand}", sidearm.Classname)
                .ApplyForEach(
                    (current, attachment) =>
                        current.AddCommandWithParameters(
                            $"{GearBuilderResources.UnitFunc} {GearBuilderResources.AddSidearmItemCommand}",
                            attachment.Classname), sidearm.Attachments)
                .AddCommandWithParameters(
                    $"{GearBuilderResources.UnitFunc} {GearBuilderResources.AddSidearmItemCommand}",
                    sidearm.Magazine?.Classname)
                .NewLine();
        }

        private static GearBuilder AddUniform(this GearBuilder builder, string classname)
        {
            return builder
                .AddCommandWithParameters($"{GearBuilderResources.UnitFunc} {GearBuilderResources.AddUniformCommand}", classname);
        }

        private static GearBuilder AddUniformItems(this GearBuilder builder, Container uniform)
        {
            if (uniform == null)
                return builder;

            return builder
                .ApplyForEach(
                    (current, item) => current.AddContainerItems(GearBuilderResources.UnitUniformFunc, item),
                    uniform.Items)
                .NewLine();
        }

        private static GearBuilder AddVest(this GearBuilder builder, string classname)
        {
            return builder
                .AddCommandWithParameters($"{GearBuilderResources.UnitFunc} {GearBuilderResources.AddVestCommand}", classname);
        }

        private static GearBuilder AddVestItems(this GearBuilder builder, Container vest)
        {
            if (vest == null)
                return builder;

            return builder
                .ApplyForEach(
                    (current, item) => current.AddContainerItems(GearBuilderResources.UnitVestFunc, item),
                    vest.Items)
                .NewLine();
        }

        private static GearBuilder AddBackpack(this GearBuilder builder, string classname)
        {
            if (String.IsNullOrEmpty(classname))
                return builder;

            return builder
                .AddCommandWithParameters($"{GearBuilderResources.UnitFunc} {GearBuilderResources.AddBackpackCommand}", classname)
                .AddCommand(GearBuilderResources.ClearBackpackCommand)
                .NewLine();
        }

        private static GearBuilder AddBackpackItems(this GearBuilder builder, Container backpack)
        {
            if (backpack == null)
                return builder;

            return builder
                .ApplyForEach(
                    (current, item) => current.AddContainerItems(GearBuilderResources.UnitBackpackFunc, item),
                    backpack.Items)
                .NewLine();
        }

        private static GearBuilder AddGoggles(this GearBuilder builder, string classname)
        {
            return builder
                .AddCommandWithParameters($"{GearBuilderResources.UnitFunc} {GearBuilderResources.AddGogglesCommand}", classname);
        }

        private static GearBuilder AddHeadgear(this GearBuilder builder, string classname)
        {
            return builder
                .AddCommandWithParameters($"{GearBuilderResources.UnitFunc} {GearBuilderResources.AddHeadgearCommand}", classname);
        }

        private static GearBuilder AddAssignedItems(this GearBuilder builder, IEnumerable<Item> items)
        {
            return builder
                .ApplyForEach(
                    (current, item) =>
                        current.AddCommandWithParameters(
                            $"{GearBuilderResources.UnitFunc} {GearBuilderResources.AssignItemCommand}", item.Classname),
                    items)
                .NewLine();
        }

        private static GearBuilder AddContainerItems(this GearBuilder builder, string unitContainer, Item item)
        {
            if (item is ItemStack)
            {
                builder.AddCommandWithParameters($"{unitContainer} {GearBuilderResources.AddItemsCommand}", item.Classname, (item as ItemStack).Count);
            }
            else
            {
                builder.AddCommandWithParameters($"{unitContainer} {GearBuilderResources.AddItemsCommand}", item.Classname, 1);
            }

            return builder;
        }
    }
}