﻿namespace Client
{
	public class UIEquipmentButton : UIReferenceButton
	{
		public ItemSlot itemSlotType = ItemSlot.Head;

		public override void OnLeftClick()
		{
			if (UIManager.TryGet("UIDragObject", out UIDragObject dragObject))
			{
				Character character = Character.localCharacter;
				if (character != null)
				{
					if (dragObject.visible)
					{
						// we check the hotkey type because we can only equip items from the inventory
						if (dragObject.hotkeyType == HotkeyType.Inventory && int.TryParse(dragObject.referenceID, out int dragIndex))
						{
							// get the item from the Inventory
							Item item = character.InventoryController.items[dragIndex];
							if (item != null)
							{
								character.EquipmentController.SendEquipRequest(dragIndex, (byte)itemSlotType);
								// clear the drag object if we succeed in equipping our item
								dragObject.Clear();
							}
							else
							{
								// clear the drag object if our item is null
								dragObject.Clear();
							}
						}
					}
					else if (!character.EquipmentController.IsSlotEmpty((byte)itemSlotType))
					{
						dragObject.SetReference(icon.texture, referenceID, hotkeyType);
					}
				}
			}
		}

		public override void OnRightClick()
		{
			Character character = Character.localCharacter;
			if (UIManager.TryGet("UIDragObject", out UIDragObject dragObject) && dragObject.visible)
			{
				dragObject.Clear();
			}
			if (character != null && hotkeyType == HotkeyType.Equipment)
			{
				Clear();

				character.EquipmentController.SendUnequipRequest((byte)itemSlotType);
			}
		}

		public override void Clear()
		{
			if (icon != null) icon.texture = null;
			if (cooldownText != null) cooldownText.text = "";
			if (amountText != null) amountText.text = "";
		}
	}
}