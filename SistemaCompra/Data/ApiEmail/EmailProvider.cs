using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SistemaCompra.Data.ApiEmail
{
	public class EmailProvider
	{
		public async Task<MailjetResponse> EmailMaijet(string recipient)
		{
			MailjetClient client = new MailjetClient("44c09895d299ce0f24573a6d6a9b81ba", "44dfcc973963ba6be402071cf975fee8");


			MailjetRequest request = new MailjetRequest
			{
				Resource = Send.Resource,
			}.Property(Send.FromEmail, "rogervalverde145@gmail.com")
		   .Property(Send.FromName, "TusComprasYa")
		   .Property(Send.Subject, "Notificaciones de compras")
		   .Property(Send.MjTemplateID, "3404840")
			.Property(Send.MjTemplateLanguage, "True")
				   .Property(Send.Recipients, new JArray {
				new JObject {
				 {"Email", recipient}
				 }
				});

			MailjetResponse response = await client.PostAsync(request);

			return response;
		}

		public async Task<MailjetResponse> EmailUpdate(string recipient,string Estado, string numOrden)
		{
			MailjetClient client = new MailjetClient("44c09895d299ce0f24573a6d6a9b81ba", "44dfcc973963ba6be402071cf975fee8");


			MailjetRequest request = new MailjetRequest
			{
				Resource = Send.Resource,
			}.Property(Send.FromEmail, "rogervalverde145@gmail.com")
		   .Property(Send.FromName, "TusComprasYa")
		   .Property(Send.Subject, "Notificaciones de compras")
		   .Property(Send.MjTemplateID, "3407922")
			.Property(Send.Vars, new JObject {
				{"estado", Estado},
				{"numOrden", numOrden}
				})
			.Property(Send.MjTemplateLanguage, "True")
				   .Property(Send.Recipients, new JArray {
				new JObject {
				 {"Email", recipient}
				 }
				});

			MailjetResponse response = await client.PostAsync(request);

			return response;
		}
	}
}
