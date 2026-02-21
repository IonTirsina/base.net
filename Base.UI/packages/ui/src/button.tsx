import * as React from "react";

export interface ButtonProps
  extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  variant?: "default" | "outline" | "ghost";
}

export function Button({
  variant = "default",
  className,
  children,
  ...props
}: ButtonProps): React.JSX.Element {
  return (
    <button data-variant={variant} className={className} {...props}>
      {children}
    </button>
  );
}
