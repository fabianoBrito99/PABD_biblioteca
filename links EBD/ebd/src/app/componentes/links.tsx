// components/LinkTree.tsx
import styles from "./links.module.css";
import Link from "next/link";
import Image from "next/image";
import {
  FaWhatsapp,
  FaInstagram,
  FaTiktok,
  FaFacebook,
  FaYoutube,
} from "react-icons/fa";

const socialLinks = [
  { icon: FaWhatsapp, url: "https://api.whatsapp.com/send?phone=6984220012" },
  {
    icon: FaInstagram,
    url: "https://www.instagram.com/igrejamissionariapresidencial/",
  },
  { icon: FaTiktok, url: "https://www.tiktok.com/@imubpresidencial" },
  { icon: FaFacebook, url: "https://www.facebook.com/imubpresidencial" },
  { icon: FaYoutube, url: "https://www.youtube.com/@amorecuidadoigreja" },
];

const groupButtons = [
  {
    name: "Semear (9-12 Anos)",
    url: "https://chat.whatsapp.com/DZZGRIFXJBn8cAMPBjzrrz",
    image: "/semear.jpg",
    gradient: "linear-gradient(135deg, #FFD700, #FF8C00)",
  },
  {
    name: "Enraizar (13-20 Anos)",
    url: "https://chat.whatsapp.com/GFhUfmoFMgK6NtnSA22r1F",
    image: "/enraizar.png",
    gradient: "linear-gradient(135deg, #008000, #32CD32)",
  },
  {
    name: "Frutificar (+ 21 Anos)",
    url: "https://chat.whatsapp.com/JyO8Axoa1oKL7u6GXIa9MI",
    image: "/frutificar.png",
    gradient: "linear-gradient(135deg, #FF4500, #DC143C)",
  },
  {
    name: "Imersão",
    url: "https://chat.whatsapp.com/JC0D4cLRfSb5WgY7qHqdqp",
    image: "/imersao.jpg",
    gradient: "linear-gradient(135deg, #1E90FF, #00008B)",
  },
];

export default function LinkEBD() {
  return (
    <div className={styles.container}>
      <Image
        src="/logo.png"
        alt="Logo"
        width={80}
        height={80}
        className={styles.logo}
      />
      <h1 className={styles.title}>
        Igreja Missionária Presidencial
        <br></br> Amor e Cuidado
      </h1>
      <p className={styles.subtitle}>Uma família para pertencer!</p>

      <h4 className={styles.rede}>Nossas redes sociais</h4>
      <div className={styles.socialIcons}>
        {socialLinks.map(({ icon: Icon, url }, index) => (
          <Link key={index} href={url} target="_blank" className={styles.icon}>
            <Icon size={24} />
          </Link>
        ))}
      </div>
      <div>
        <Image
          src="/ebd.jpg"
          alt="image"
          width={400}
          height={200}
          className={styles.EbdImage}
        />
      </div>

      <div className={styles.entre}>
        <p>
          <strong>Participe da Escola Bíblica Dominical! 📖✨</strong> <br></br>{" "}
          <br></br> A Escola Bíblica Dominical acontece todos os domingos às 8h
          da manhã. Para receber mais informações e se conectar com outras
          pessoas da sua faixa etária, entre no grupo correspondente à sua
          idade:
          <br></br>
          <br></br>
           <strong>Observação: </strong>O grupo de Imersão é destinado ao batismo e recepção de novos
          membros. Se você deseja se aprofundar na fé e fazer parte da nossa
          comunidade, clique no botão acima e junte-se a nós!
        </p>
      </div>

      <div className={styles.buttonsContainer}>
        {groupButtons.map(({ name, url, image, gradient }, index) => (
          <Link
            key={index}
            href={url}
            target="_blank"
            className={styles.button}
            style={{ background: gradient }}
          >
            <Image
              src={image}
              alt={name}
              width={40}
              height={40}
              className={styles.buttonImage}
            />
            {name}
          </Link>
        ))}
      </div>
    </div>
  );
}
