﻿using ApiClient.Enums;

namespace ApiClient.Dtos
{
	public class Element
	{
		public TextValueDto Distance { get; set; }
		public TextValueDto Duration { get; set; }
		public GoogleStatusCode Status { get; set; }
	}
}