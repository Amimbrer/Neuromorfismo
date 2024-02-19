﻿using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using WebMedicina.BackEnd.Dto;
using WebMedicina.BackEnd.ServicesDependencies;

namespace WebMedicina.BackEnd.Service; 
public class EmailService : IEmailService {
    private readonly EmailConfig _config;    

    public EmailService(IOptionsSnapshot<EmailConfig> config) { 
        _config = config.Value;
    }

    /// <summary>
    /// Enviamos mensaje de correo
    /// </summary> 
    public void Send(string asunto, string mensaje, MemoryStream imagen) {
         // Generamos mensaje del correo
        MailMessage mail = new() {
            From = new(_config.Usuario),
            Subject = asunto,
            IsBodyHtml = true
        };

        // Creamos vista con el body de correo
        AlternateView vistaBody = AlternateView.CreateAlternateViewFromString(mensaje, null, "text/html");

        // Insertamos la captura de la evolucion a la vista
        LinkedResource imagenLinkeada = new(imagen, "image/png") { 
            ContentId = "capturaEvo",
        };
        imagenLinkeada.ContentType.Name = "Captura avance paciente";
        vistaBody.LinkedResources.Add(imagenLinkeada);
        mail.AlternateViews.Add(vistaBody);



        // Mapeamos los destinatarios
        foreach (string correo in _config.Destinatarios) {
            mail.To.Add(new(correo));
        }

        // Conectamos con el servidor de email
        SmtpClient smtp = new(_config.Host, _config.Puerto) {
            Credentials = new NetworkCredential(_config.Usuario, _config.Contrasena),
            EnableSsl = _config.Ssl,
            UseDefaultCredentials = _config.DefaultCredencials
        };

        smtp.SendAsync(mail, null);
    }
}
